using System;
using System.Linq;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Logs;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Core.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect: MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
                throw new ArgumentException(AspectMessages.WrongLoggerType);
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }
        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            var logDetailWithException = LogDetailHelper.GetLogDetailWithException(invocation, e.Message);
            logDetailWithException.ExceptionMessage = e is AggregateException exception ? string.Join(Environment.NewLine, exception.InnerExceptions.Select(x => x.Message)) : e.Message;
            logDetailWithException.User = LogDetailHelper.GetCurrentUser(_httpContextAccessor);
            _loggerServiceBase.Error(JsonConvert.SerializeObject(logDetailWithException));
        }
    }
}