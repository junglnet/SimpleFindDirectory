using System;
using NLog;
using ILogger = Bochky.FindDirectory.Common.Interfaces.ILogger;

namespace Bochky.Utils.Logger 
{ 
    public class NLogLogger : ILogger
    {
        private readonly NLog.Logger _logger;
        public NLogLogger(string name)
        {
            _logger = LogManager.GetLogger(name);            
        }

        #region Implementation of ILogger

        public void LogError(Exception ex)
        {
            LogError(ex, null);
        }

        public void LogError(Exception ex, string message)
        {
            if (message != null)
                _logger.Log(LogLevel.Error, ex, message);
            else
                _logger.Log(LogLevel.Error, ex);
        }

        public void LogInfo(string message)
        {
            _logger.Log(LogLevel.Info, message);
        }

        public void LogDebug(string message)
        {
            _logger.Log(LogLevel.Debug, message);
        }

        #endregion
    }
}
