using System.IO;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

// [REF] 
//  qiita: 

namespace Nitou {
    public static class Logger {
        
        private static bool _isLoggingEnabled = true;

        // 現在のLogLevel
        internal static LogLevel CurrentLogLevel { get; private set; } = LogLevel.Debug;


        /// ----------------------------------------------------------------------------
        // Public Method


        /// ----------------------------------------------------------------------------
        // Private Method

        [DebuggerStepThrough]
        private static void InternalLog(LogLevel logLevel, string message, LoggerTag tag,
            string filePath = "", int lineNumber = 0, string memberName = "") {

            if (!_isLoggingEnabled) {
                Debug.Log("YourProject: Logging is disabled.");
                return;
            }

            CurrentLogLevel = logLevel;
            var logMessage = FormatLogMessage(logLevel, tag, message, filePath, lineNumber, memberName);
            OutputLogMessage(logLevel, logMessage);
        }


        private static string FormatLogMessage(LogLevel logLevel, LoggerTag tag,
            string message, string filePath, int lineNumber, string memberName) {
            return
                $"YourProject: [{logLevel}] [{tag}] {message} (at {Path.GetFileName(filePath)}:{lineNumber} in {memberName})";
        }

        private static void OutputLogMessage(LogLevel logLevel, string logMessage) {

            switch (logLevel) {
                case LogLevel.Debug:
                case LogLevel.Info:
                    break;
                case LogLevel.Warning:
                    break;
                case LogLevel.Error:
                    break;
                default:
                    break;
            }

            Debug.Log(logMessage);
        }
    }
}
