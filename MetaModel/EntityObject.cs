using System.Collections.Generic;

namespace LossDataExtractor.MetaModel
{
    public class EntityObject : EntityField
    {
        public List<EntityField> EntityFields { get; set; }
        
        

        public EntityObject(string fieldName) : this()
        {
            FieldName = fieldName;
        }

        public EntityObject()
        {
            EntityFields = new List<EntityField>();
        }

        public override string ToString()
        {
            var str = $"Object : {FieldName}";
            foreach (var entityField in EntityFields)
            {
                str += $"\n EntityField : {entityField.ToString()}";
            }

            return str;
        }
    }
}