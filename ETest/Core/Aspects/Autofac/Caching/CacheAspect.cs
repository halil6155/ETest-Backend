using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching.Abstract;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect:MethodInterception
    {
        private readonly ICacheManager _cacheManager;
        private readonly int _duration;

        public CacheAspect(int duration = 60)
        {
            _cacheManager = _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
            _duration = duration;
        }
        private string CreateKey(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var parameters = "";
            for (var i = 0; i < arguments.Count; i++)
            {
                parameters += (parameters == "" ? "" : ",") + (arguments[i]?.GetType().IsClass ?? false ? GetPropertyList(arguments[i]) : arguments[i] ?? "Null");
            }
            return $"{methodName}({parameters})";
        }
        private static string GetPropertyList(object entity)
        {
            return entity == null ? "" :
                entity.GetType().GetProperties()
                    .Select(property => property.GetValue(entity) ?? "Null")
                    .Aggregate("", (current, value) => current + (current == "" ? "" : ",") + $"{value}");

        }
        protected override void OnBefore(IInvocation invocation)
        {
            var key = CreateKey(invocation);
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration); 
            }
        }

 
}
