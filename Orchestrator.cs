
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using LossDataExtractor.Data;
using LossDataExtractor.Writer;

namespace LossDataExtractor
{

    public static class Orchestrator
    {
        private static ILog logger = LogFactory.GetLogInstance("ORCHESTRATION");

        public static void ReportPortfolioBreaches()
        {
            var reportableData = GenerateReportableData();
            logger.Info("Writing CSV file");
            Test(GenerateReportableData());
            IWrite csvWriter = new CSVWriter();
            csvWriter.WriteToFile(reportableData, "./breaches.csv");
        }

        private static void Test(IEnumerable<Object> datas)
        {
            foreach (Object data in datas)
            {
                var props = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                List<string> content;
                content = new List<string>();
                content.AddRange(props.Select(p =>
                {
                    Console.WriteLine(p.ToString());
                    return p.GetValue(data, null).ToString();
                }));
            }
        }

        private static IEnumerable<ReportableData> GenerateReportableData()
        {
            var toReturn = new List<ReportableData>();
            for (int i = 0; i <= 10; i++)
            {
                var reportableData = new ReportableData();
                reportableData.ClientId = "Test Client";
                reportableData.PortfolioId = "000111000";
                reportableData.ReturnPct = 20.0;
                reportableData.AsOfDate = "Some Date";
                reportableData.LossResultId = 101;
                var twrSeries = new List<TwrSeriesReportableData>();
                for (int j = 0; j <= 10; j++)
                {
                    var twr = GenerateTwrSeriesReportableData();
                    twrSeries.Add(twr);
                }

                reportableData.TwrSeries = twrSeries;
                toReturn.Add(reportableData);
            }

            return toReturn;
        }

        private static TwrSeriesReportableData GenerateTwrSeriesReportableData()
        {
            var twr = new TwrSeriesReportableData();
            twr.SecId = "SecId";
            twr.SecName = "SecName";
            twr.PeriodBasTwr = 200.0;
            twr.AccPeriodBasTwrAtMarketPrice = 100;
            twr.EopBasHoldingValueAtMarketPrice = 10.0;
            return twr;
        }
    }
}