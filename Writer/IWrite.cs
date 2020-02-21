using System.Collections.Generic;
using LossDataExtractor.Data;
using LossDataExtractor.MetaModel;

namespace LossDataExtractor.Writer
{
    public interface IWrite
    {
        void WriteToFile<T>(IEnumerable<T> results, Header model);
    }
}