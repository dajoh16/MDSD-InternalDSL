using System.Collections.Generic;

namespace LossDataExtractor.MetaModel
{
    public class EntityObject : EntityField
    {
        public List<EntityField> EntityFields { get; set; }
        
        public string FieldName { get; set; }

        public EntityObject(string fieldName) : this()
        {
            FieldName = fieldName;
        }

        public EntityObject()
        {
            EntityFields = new List<EntityField>();
        }
    }
}