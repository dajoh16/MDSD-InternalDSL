using log4net;

namespace LossDataExtractor
{
    public static class LogFactory
    {
        public static ILog GetLogInstance(string name)
        {
            return LogManager.GetLogger(name);
        }
    }
}