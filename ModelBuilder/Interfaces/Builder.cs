
namespace LossDataExtractor.ModelBuilder.Interfaces
{
    public interface Builder
    {
        /*
         * The initial method to initialize the building process of a csv meta model
         */
        Builder Header(string CsvFileName);

        /*
         * Build the outer most object - This is the root object when reading or writing using the meta model
         */
        ObjectBuilder Object();

    }
}