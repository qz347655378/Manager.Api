using Common.Enum;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Layout.Pattern;
using Microsoft.AspNetCore.Http;
using Models.System;
using System;
using System.IO;

namespace API.Core
{
    public class LogHelper
    {
        private static readonly LogHelper Instance = new LogHelper();
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger("adoLog");
        public LogHelper()
        {
            var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(basePath ?? string.Empty, "\\Config\\Log4NetConfig.xml")));
        }

        public static void Write(string message, LogType logType, Exception exception, HttpContext context)
        {
            var logInfo = new SysLog
            {
                Message = message,
                Api = context.Request.Path,
                RequestMethod = context.Request.Method,
                ClientIp = context.Request.Headers["X-Forwarded-For"],
                Level = nameof(logType),
                ResponseStatus = context.Response.StatusCode.ToString(),
                Timestampp = DateTime.Now
            };
            if (exception == null)
            {
                _log.Info(logInfo);
            }
            else
            {
                logInfo.Exception = exception.StackTrace;
                _log.Error(logInfo);
            }

        }

        //private static void SetOtherInfo(HttpContext context)
        //{
        //    var logInfo = new SysLog
        //    {
        //    };

        //}

    }

    public class ActionLayouPattern : PatternLayout
    {
        public ActionLayouPattern()
        {
            AddConverter("", typeof(ActionConverter));
        }
    }

    public class ActionConverter : PatternLayoutConverter
    {

        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            var sysLogIngo = loggingEvent.MessageObject as SysLog;
            if (sysLogIngo == null)
            {
                writer.Write("");
            }
            else
            {
                switch (this.Option)
                {
                    case nameof(SysLog.Api):
                        writer.Write(sysLogIngo.Api);
                        break;
                    case nameof(SysLog.ClientIp):
                        writer.Write(sysLogIngo.ClientIp);
                        break;
                    case nameof(SysLog.Exception):
                        writer.Write(sysLogIngo.Exception);
                        break;
                    case nameof(SysLog.Level):
                        writer.Write(sysLogIngo.Level);
                        break;
                    case nameof(SysLog.RequestMethod):
                        writer.Write(sysLogIngo.RequestMethod);
                        break;
                    case nameof(SysLog.ResponseStatus):
                        writer.Write(sysLogIngo.ResponseStatus);
                        break;
                }
            }
        }
    }
}
