﻿using System.Collections.Generic;
using LossDataExtractor.MetaModel;
using LossDataExtractor.ModelBuilder.Interfaces;

namespace LossDataExtractor.ModelBuilder
{
    public class CsvModelBuilder : Builder, ObjectBuilder
    {
        private Header root;

        private EntityObject rootObject;

        private List<EntityField> currentFields;

        public static Builder GetBuilder()
        {
            return new CsvModelBuilder();
        }

        private CsvModelBuilder()
        {
            
        }
        
        public Builder Header(string CsvFileName)
        {
            root = new Header(CsvFileName);
            return this;
        }

        public ObjectBuilder Object()
        {   
            rootObject = new EntityObject();
            root.RootObject = rootObject;
            currentFields = new List<EntityField>();
            return this;
        }

        public ObjectBuilder Object(string FieldName)
        {
            var objectBuilder = new InternalObjectBuilder(this, FieldName);
            currentFields.Add(objectBuilder.getObject());
            return objectBuilder;
        }

        public ObjectBuilder String(string FieldName)
        {
            var stringField = new EntityStringField(FieldName);
            currentFields.Add(stringField);
            return this;
        }

        public ObjectBuilder Number(string FieldName)
        {
            var numberField = new EntityNumberField(FieldName);
            currentFields.Add(numberField);
            return this;
        }

        public ObjectBuilder List(string FieldName)
        {
            var builder = new InternalListBuilder(this,FieldName);
            currentFields.Add(builder.GetList());
            return builder;
        }

        public Header Build()
        {
            root.RootObject.EntityFields = currentFields;
            return root;
        }
    }
}