namespace LossDataExtractor.MetaModel
{
    public class EntityNumberField : EntityField
    {
        public string FieldName { get; set; }

        public EntityNumberField(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}