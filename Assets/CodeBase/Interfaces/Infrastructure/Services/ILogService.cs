namespace CodeBase.Infrastructure.AssetManagement.Services
{
    public interface ILogService
    {
        void Log(string message);
        void LogError(string message);
        void LogWarning(string message);
        void LogException(System.Exception exception);
        void LogFormat(string format, params object[] args);
        void LogErrorFormat(string format, params object[] args);
        void LogWarningFormat(string format, params object[] args);
    }
}