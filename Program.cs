

using System;
using System.Linq;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using LossDataExtractor.MetaModel;
using LossDataExtractor.ModelBuilder;

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
                                        Build();


            /*
            var rootAppender = ((Hierarchy)LogManager.GetRepository())
                .Root.Appenders.OfType<FileAppender>()
                .FirstOrDefault();
            string filename = rootAppender != null ? rootAppender.File : string.Empty;
            Console.WriteLine("Loss Data Extractor will log to the following file: " + filename);
            Console.WriteLine("Starting Loss Data Extractor");
            logger.Info("Starting Loss Data Extractor");
            Orchestrator.ReportPortfolioBreaches();
            Console.WriteLine("Loss Data Extraction Complete. Press Enter to close.");
            logger.Info("Loss Data Extraction Complete.");
            Console.ReadKey(true);
            */
        }
    }
}