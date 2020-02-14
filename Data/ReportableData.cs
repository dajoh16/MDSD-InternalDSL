using System;
using System.Collections.Generic;
using log4net;
using log4net.Repository.Hierarchy;

namespace LossDataExtractor.Data
{
    public class ReportableData
    {
        public string PortfolioId { get; set; }
        public string ClientId { get; set; }
        public long LossResultId { get; set; }

        public string AsOfDate { get; set; }
        public double ReturnPct { get; set; }
        public List<TwrSeriesReportableData> TwrSeries { get; set; }

    }

}