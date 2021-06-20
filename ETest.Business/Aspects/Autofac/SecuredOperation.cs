using System;
using System.Linq;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Exceptions;
using Core.Extensions;
using Core.Logs;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ETest.Business.Aspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private readonly string[] _roles;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoggerServiceBase _loggerServiceBase;
        public SecuredOperation(string roles, Type loggerServiceBase)
        {
            if (loggerServiceBase.BaseType != typeof(LoggerServiceBase))
                throw new Exception(AspectMessages.WrongLoggerType);
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerServiceBase);
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var roles = _httpContextAccessor.HttpContext.User.ClaimRoles();
            var nameIdentifier = _httpContextAccessor.HttpContext.User.ClaimNameIdentifier();
            if (nameIdentifier <= 0) throw new AuthenticationException(AspectMessages.UserNotLogin);
            foreach (var role in roles)
            {
                if (_roles.Contains(role))
                    return;
            }

            var logDetailWithException =
                LogDetailHelper.GetLogDetailWithException(invocation, AspectMessages.UserHasNoAuthority);
            logDetailWithException.User = $"UserId----{nameIdentifier}";
            _loggerServiceBase.Error(JsonConvert.SerializeObject(logDetailWithException));
            throw new AdminSecurityException(AspectMessages.UserHasNoAuthority);


        }
    }
}