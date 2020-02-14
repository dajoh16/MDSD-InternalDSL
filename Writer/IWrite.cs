using System.Collections.Generic;
using LossDataExtractor.Data;

namespace LossDataExtractor.Writer
{
    public interface IWrite
    {
        void WriteToFile(IEnumerable<ReportableData> results, string path);
    }
}