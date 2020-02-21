

using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using LossDataExtractor.Data;
using LossDataExtractor.MetaModel;
using LossDataExtractor.ModelBuilder;
using LossDataExtractor.Writer;

namespace LossDataExtractor
{
    internal class Program
    {
        private static ILog logger = LogFactory.GetLogInstance("MAIN");
        public static void Main(string[] args)
        {
            var builder = CsvModelBuilder.GetBuilder();
            var model = builder.Header("Csv.csv").
                Object().
                    String("PortfolioId").
                    String("ClientId").
                    Number("LossResultId").
                    String("AsOfDate").
                    Number("ReturnPct").
                        List("TwrSeries").
                            String("SecId").
                            Number("AccPeriodBasTwrAtMarketPrice").
                            Build();
            var model2 = builder.Header("Csv.csv").
                Object().
                String("PortfolioId").
                String("ClientId").
                Number("LossResultId").
                String("AsOfDate").
                Number("ReturnPct").
                Object("NestedObject").String("NestedObjectDesc").Number("NestedObjectValue").
                Build();
            var model3 = builder.Header("Csv.csv").
                Object().
                String("PortfolioId").
                String("ClientId").
                Number("LossResultId").
                String("AsOfDate").
                Number("ReturnPct").
                    List("TwrSeries").
                        String("SecId").
                        Number("AccPeriodBasTwrAtMarketPrice").
                        List("NestedObjList").
                            String("NestedObjectDesc").
                            Number("NestedObjectValue").
                Build();
            var reportableData = GenerateReportableData();
            var writer = new CSVWriter();
            writer.WriteToFile<ReportableData>(reportableData,model3);
        }
        
        private static IEnumerable<ReportableData> GenerateReportableData()
        {
            var toReturn = new List<ReportableData>();
            for (int i = 0; i <= 10; i++)
            {
                var reportableData = new ReportableData();
                reportableData.ClientId = "Test Client";
                reportableData.PortfolioId = "000111000";
                reportableData.ReturnPct = i;
                reportableData.AsOfDate = "Some Date";
                reportableData.LossResultId = 101;
                var twrSeries = new List<TwrSeriesReportableData>();
                for (int j = 0; j <= 10; j++)
                {
                    var twr = GenerateTwrSeriesReportableData();
                    twr.AccPeriodBasTwrAtMarketPrice = j;
                    twrSeries.Add(twr);
                }

                reportableData.TwrSeries = twrSeries;
                
                var nestedObj = new NestedObject();
                nestedObj.NestedObjectDesc = "Some Desc";
                nestedObj.NestedObjectValue = 8850;
                reportableData.NestedObject = nestedObj;
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
            twr.NestedObjList = GenerateNestedObjList();
            return twr;
        }

        private static List<NestedObject> GenerateNestedObjList()
        {
            var list = new List<NestedObject>();
            for (int i = 0; i < 5; i++)
            {
                var nest = new NestedObject();
                nest.NestedObjectDesc = "Some Desc of a Nested Obj";
                nest.NestedObjectValue = i;
                list.Add(nest);
            }

            return list;
        }
    }
}