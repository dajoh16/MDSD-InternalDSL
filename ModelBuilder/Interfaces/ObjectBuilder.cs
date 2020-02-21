using LossDataExtractor.MetaModel;

namespace LossDataExtractor.ModelBuilder.Interfaces
{
    public interface ObjectBuilder
    {
        /*
         * Build a nested object.
         * The FieldName parameter must match the exact variable name of the nested object
         */
        ObjectBuilder Object(string FieldName);
        /*
         * Build a field containing a String Value.
         * The FieldName parameter must match the exact variable name of the string
         */
        ObjectBuilder String(string FieldName);
        /*
        * Build a field containing a Number Value.
        * The FieldName parameter must match the exact variable name of the Number
        */
        ObjectBuilder Number(string FieldName);
        /*
        * Build a field containing a List of objects.
        * The FieldName parameter must match the exact variable name of the List
         * After calling this method explicit Fields of String, Number or Object can be written out from the list objects
        */
        ObjectBuilder List(string FieldName);
       /*
         * Build the metamodel.
         * Returns the root object of the model.
         */
        Header Build();
    }
}