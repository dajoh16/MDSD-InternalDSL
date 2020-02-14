using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LossDataExtractor.Data;

namespace LossDataExtractor.Writer
{
    public class CSVWriter : IWrite
    {
        public CSVWriter()
        {
        }

        public void WriteToFile(IEnumerable<ReportableData> results, string path)
        {
            Type reportableDataType = typeof(ReportableData);
            var reportableDataProps = reportableDataType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                

            Type TwrSeriesReportableDataType = typeof(TwrSeriesReportableData);
            var TwrSeriesReportableProps =
                TwrSeriesReportableDataType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                
           
            using (var writer = new StreamWriter(path))
            {
                WriteHeadersToFile(reportableDataProps, TwrSeriesReportableProps, writer);

                foreach (var item in results)
                {
                    WriteReportableDataToFile(reportableDataProps, item, TwrSeriesReportableProps, writer);
                }
            }
        }
        

        private void WriteReportableDataToFile(PropertyInfo[] reportableDataProps, ReportableData item,
            PropertyInfo[] TwrSeriesReportableProps, StreamWriter writer)
        {
            List<string> content;
            content = new List<string>();
            content.AddRange(reportableDataProps.Select(p =>
            {
                if (IsSimple(p.PropertyType))
                {
                    return p.GetValue(item, null).ToString();
                }

                return null;
            }));

            foreach (var twrSeriesReportableData in item.TwrSeries)
            {
                WriteTwrSeriesReportableDataToFile(content, TwrSeriesReportableProps, twrSeriesReportableData, writer,
                    reportableDataProps);
            }
        }

        private void WriteTwrSeriesReportableDataToFile(List<string> content, PropertyInfo[] TwrSeriesReportableProps,
            TwrSeriesReportableData twrSeriesReportableData, StreamWriter writer, PropertyInfo[] reportableDataProps)
        {
            content.AddRange(TwrSeriesReportableProps.Select(p =>
            {
                if (IsSimple(p.PropertyType))
                {
                    return p.GetValue(twrSeriesReportableData, null).ToString();
                }

                return null;
            }));
            writer.WriteLine(string.Join(";", content.Where(s => !String.IsNullOrEmpty(s))));
            content.Clear();
            content.AddRange(
                reportableDataProps.Select(p =>
                {
                    if (IsSimple(p.PropertyType))
                    {
                        return " ";
                    }

                    return null;
                }));
        }

        private void WriteHeadersToFile(PropertyInfo[] reportableDataProps, PropertyInfo[] TwrSeriesReportableProps,
            StreamWriter writer)
        {
            List<string> headers = new List<string>();
            headers.AddRange(reportableDataProps.Select(p =>
            {
                if (IsSimple(p.PropertyType))
                {
                    return p.Name;
                }

                return null;
            }));

            headers.AddRange(TwrSeriesReportableProps.Select(p =>
            {
                if (IsSimple(p.PropertyType))
                {
                    return p.Name;
                }

                return null;
            }));

            writer.WriteLine(string.Join(";", headers.Where(s => !String.IsNullOrEmpty(s))));
        }

        private static bool IsSimple(Type type)
        {
            return type.IsPrimitive || type.Equals(typeof(string));
        }
   
    }
    
}