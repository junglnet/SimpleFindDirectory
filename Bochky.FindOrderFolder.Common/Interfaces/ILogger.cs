using System;

namespace Bochky.FindDirectory.Common.Interfaces
{
    public interface ILogger
    {
        void LogDebug(string message);
        void LogError(Exception ex);
        void LogError(Exception ex, string message);
        void LogInfo(string message);
    }
}
