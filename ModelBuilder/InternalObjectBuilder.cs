using System.Collections.Generic;
using LossDataExtractor.MetaModel;
using LossDataExtractor.ModelBuilder.Interfaces;

namespace LossDataExtractor.ModelBuilder
{
    public class InternalObjectBuilder : ObjectBuilder
    {
        private ObjectBuilder _parent;

        private EntityObject _entityObject;
        
        private List<EntityField> _currentFields;

        public InternalObjectBuilder(ObjectBuilder parent, string internalObjectFieldName)
        {
            this._currentFields = new List<EntityField>();
            this._parent = parent;
            this._entityObject = new EntityObject(internalObjectFieldName);
        }

        public EntityObject getObject()
        {
            return _entityObject;
        }

        public ObjectBuilder Object(string FieldName)
        {
            var objectBuilder = new InternalObjectBuilder(this, FieldName);
            _currentFields.Add(objectBuilder.getObject());
            return objectBuilder;
        }

        public ObjectBuilder String(string FieldName)
        {
            var stringField = new EntityStringField(FieldName);
            _currentFields.Add(stringField);
            return this;
        }

        public ObjectBuilder Number(string FieldName)
        {
            var numberField = new EntityNumberField(FieldName);
            _currentFields.Add(numberField);
            return this;
        }

        public ObjectBuilder List(string FieldName)
        {
            var builder = new InternalListBuilder(this,FieldName);
            _currentFields.Add(builder.GetList());
            return builder;
        }

       
        public Header Build()
        {
            _entityObject.EntityFields = _currentFields;
            return _parent.Build();
        }
    }
}