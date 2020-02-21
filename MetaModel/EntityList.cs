using System.Collections.Generic;

namespace LossDataExtractor.MetaModel
{
    public class EntityList : EntityField
    {
        public List<EntityField> EntityFields { get; set; }
        
        

        public EntityList(string fieldName) : this()
        {
            FieldName = fieldName;
        }

        public EntityList()
        {
            EntityFields = new List<EntityField>();
        }

        public override string ToString()
        {
            var str = $"List : {FieldName}";
            foreach (var entityField in EntityFields)
            {
                str += $"\n\t {entityField.ToString()}";
            }

            return str;
        }
    }
}