using System;
using System.Diagnostics;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Logs;
using Newtonsoft.Json;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private readonly int _interval;
        private readonly Stopwatch _stopwatch;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoggerServiceBase _loggerServiceBase;
        public PerformanceAspect(int interval, Type loggerService)
        {
            _interval = interval;

            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }
        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                var logDetailWithException = LogDetailHelper.GetLogDetailWithException(invocation,null);
                logDetailWithException.ExceptionMessage = $"Performance {_interval} ----- {_stopwatch.Elapsed.TotalSeconds}";
                logDetailWithException.User = LogDetailHelper.GetCurrentUser(_httpContextAccessor);
                _loggerServiceBase.Fatal(JsonConvert.SerializeObject(logDetailWithException));
            }
            _stopwatch.Reset();
        }

    }
}