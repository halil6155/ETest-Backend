using System.Collections.Generic;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Microsoft.AspNetCore.Http;

namespace Core.Logs
{
    public class LogDetailHelper
    {
        public static LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            if (invocation.Arguments.Length > 0 && invocation.Arguments[0] != null)
            {
                for (int i = 0; i < invocation.Arguments.Length; i++)
                {
                    logParameters.Add(new LogParameter
                    {
                        Value = invocation.Arguments[i],
                        Type = invocation.Arguments[i].GetType().Name,
                        Name = invocation.GetConcreteMethod().GetParameters()[i].Name
                    });
                }

            }
            var logDetail = new LogDetail { FullName = $"{invocation.Method.ReflectedType?.FullName}", MethodName = invocation.Method.Name, Parameters = logParameters };
            return logDetail;
        }
        public static LogDetailWithException GetLogDetailWithException(IInvocation invocation, string exceptionMessage)
        {
            var logDetail = GetLogDetail(invocation);
            return new LogDetailWithException { Parameters = logDetail.Parameters, MethodName = logDetail.MethodName, ExceptionMessage = exceptionMessage };
        }
        public static string GetCurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext.User.Identity != null
                ? httpContextAccessor.HttpContext.User.Identity != null &&
                  (httpContextAccessor.HttpContext?.User.Identity.Name == null)
                      ? "?"
                      : httpContextAccessor.HttpContext.User.Identity.Name
                : "<Not User>";
        }
    }
}