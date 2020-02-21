﻿namespace LossDataExtractor.MetaModel
{
    public class EntityStringField : EntityField
    {
        public EntityStringField(string fieldName)
        {
            FieldName = fieldName;
        }

        

        public override string ToString()
        {
            return $"String : {FieldName}";
        }
    }
}