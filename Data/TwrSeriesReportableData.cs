using System;
using log4net;


namespace LossDataExtractor.Data
{
    public class TwrSeriesReportableData
    {
        public string SecId { get; set; }
        public string SecName { get; set; }

        public double AccPeriodBasTwrAtMarketPrice { get; set; }
        public double EopBasHoldingValueAtMarketPrice { get; set; }
        public double PeriodBasTwr { get; set; }

    }
}