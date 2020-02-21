using System.Collections.Generic;
using LossDataExtractor.MetaModel;
using LossDataExtractor.ModelBuilder.Interfaces;

namespace LossDataExtractor.ModelBuilder
{
    public class InternalListBuilder : ObjectBuilder
    {
        private ObjectBuilder _parent;
        private List<EntityField> _currentFields;
        private EntityList _entityList;
        
        
        public InternalListBuilder(ObjectBuilder parent, string internalListFieldName)
        {
            this._currentFields = new List<EntityField>();
            this._parent = parent;
            this._entityList = new EntityList(internalListFieldName);
        }

        public EntityList GetList()
        {
            return this._entityList;
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
            _entityList.EntityFields = _currentFields;
            return _parent.Build();
        }
    }
}