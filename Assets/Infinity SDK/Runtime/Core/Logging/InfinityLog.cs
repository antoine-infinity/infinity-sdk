using System;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infinity.Runtime.Core.Logging
{
    public static class InfinityLog
    {
        private static string InfoColor
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return "";
#else
                return "<color=#2DE55A>";
#endif
            }
        }

        private static string WarningColor
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return "";
#else
                return "<color=#B74824>";
#endif
            }
        }

        private static string ErrorColor
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return "";
#else
                return "<color=#EA1C0D>";
#endif
            }
        }

        private static string EndColor
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return "";
#else
                return "</color>";
#endif
            }
        }

        private enum LogType
        {
            None,
            Client,
            Server
        }

        private enum LogLevel
        {
            Info,
            Warning,
            Error
        }

        #region Generic

        public static void Info<T>(T source, string message, bool shouldLog = true) where T : Object
        {
            if (!shouldLog) return;
            WriteLog(source, message);
        }

        public static void Server<T>(T source, string message, bool shouldLog = true) where T : Object
        {
            if (!shouldLog) return;
            WriteLog(source, message, LogType.Server);
        }

        public static void Server(Type source, string message, bool shouldLog = true)
        {
            if (!shouldLog) return;
            WriteLog(source, message, LogType.Server);
        }

        public static void Client<T>(T source, string message, bool shouldLog = true) where T : Object
        {
            if (!shouldLog) return;
            WriteLog(source, message, LogType.Client);
        }

        public static void Warning<T>(T source, string message, bool shouldLog = true) where T : Object
        {
            if (!shouldLog) return;
            WriteLog(source, message, LogType.None, LogLevel.Warning);
        }

        public static void ServerWarning<T>(T source, string message, bool shouldLog = true) where T : Object
        {
            if (!shouldLog) return;
            WriteLog(source, message, LogType.Server, LogLevel.Warning);
        }

        public static void ClientWarning<T>(T source, string message, bool shouldLog = true) where T : Object
        {
            if (!shouldLog) return;
            WriteLog(source, message, LogType.Client, LogLevel.Warning);
        }

        public static void Error<T>(T source, string message, bool shouldLog = true) where T : Object
        {
            if (!shouldLog) return;
            WriteLog(source, message, LogType.None, LogLevel.Error);
        }

        public static void ServerError<T>(T source, string message, bool shouldLog = true) where T : Object
        {
            if (!shouldLog) return;
            WriteLog(source, message, LogType.Server, LogLevel.Error);
        }

        public static void ClientError<T>(T source, string message, bool shouldLog = true) where T : Object
        {
            if (!shouldLog) return;
            WriteLog(source, message, LogType.Client, LogLevel.Error);
        }

        #endregion

        #region Typed

        public static void Info(Type source, string message, bool shouldLog = true)
        {
            if (!shouldLog) return;
            WriteLog(source, message);
        }

        public static void Warning(Type source, string message, bool shouldLog = true)
        {
            if (!shouldLog) return;
            WriteLog(source, message, LogType.None, LogLevel.Warning);
        }

        public static void Error(Type source, string message, bool shouldLog = true)
        {
            if (!shouldLog) return;
            WriteLog(source, message, LogType.None, LogLevel.Error);
        }

        #endregion

        private static void WriteLog<T>(T source, string message, LogType logType = LogType.None,
            LogLevel level = LogLevel.Info) where T : Object
        {
            var sb = new StringBuilder();

            switch (level)
            {
                case LogLevel.Info:
                    sb.Append($"{InfoColor}(INFO) ");
                    if (logType != LogType.None) sb.Append($"[{logType.ToString()}] ");
                    sb.Append($"[{typeof(T).Name}]{EndColor} {message}");
                    Debug.Log(sb.ToString(), source);
                    break;
                case LogLevel.Warning:
                    sb.Append($"{WarningColor}(WARN) ");
                    if (logType != LogType.None) sb.Append($"[{logType.ToString()}] ");
                    sb.Append($"[{typeof(T).Name}]{EndColor} {message}");
                    Debug.LogWarning(sb.ToString(), source);
                    break;
                case LogLevel.Error:
                    sb.Append($"{ErrorColor}(ERR) ");
                    if (logType != LogType.None) sb.Append($"[{logType.ToString()}] ");
                    sb.Append($"[{typeof(T).Name}]{EndColor} {message}");
                    Debug.LogError(sb.ToString(), source);
                    break;
            }
        }

        private static void WriteLog(Type source, string message, LogType logType = LogType.None,
            LogLevel level = LogLevel.Info)
        {
            var sb = new StringBuilder();

            switch (level)
            {
                case LogLevel.Info:
                    sb.Append($"{InfoColor}(INFO) ");
                    if (logType != LogType.None) sb.Append($"[{logType.ToString()}] ");
                    sb.Append($"[{source.Name}]{EndColor} {message}");
                    Debug.Log(sb.ToString());
                    break;
                case LogLevel.Warning:
                    sb.Append($"{WarningColor}(WARN) ");
                    if (logType != LogType.None) sb.Append($"[{logType.ToString()}] ");
                    sb.Append($"[{source.Name}]{EndColor} {message}");
                    Debug.LogWarning(sb.ToString());
                    break;
                case LogLevel.Error:
                    sb.Append($"{ErrorColor}(ERR) ");
                    if (logType != LogType.None) sb.Append($"[{logType.ToString()}] ");
                    sb.Append($"[{source.Name}]{EndColor} {message}");
                    Debug.LogError(sb.ToString());
                    break;
            }
        }
    }
}