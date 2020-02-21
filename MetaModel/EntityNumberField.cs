namespace LossDataExtractor.MetaModel
{
    public class EntityNumberField : EntityField
    {
        

        public EntityNumberField(string fieldName)
        {
            FieldName = fieldName;
        }
        
        public override string ToString()
        {
            return $"Number : {FieldName}";
        }
    }
}