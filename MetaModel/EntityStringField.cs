namespace LossDataExtractor.MetaModel
{
    public class EntityStringField : EntityField
    {
        public EntityStringField(string fieldName)
        {
            FieldName = fieldName;
        }

        public string FieldName { get; set; }
    }
}