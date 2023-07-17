using Castle.DynamicProxy;
using ClassicArchitecture.Core.CrossCuttingConcerns.Caching;
using ClassicArchitecture.Core.Utilities.Interceptors;
using ClassicArchitecture.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private readonly int _duration;
        private readonly ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            if(invocation.Method.ReflectedType != null)
            {
                var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
                var arguments = invocation.Arguments.ToList();
                var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
                
                if(_cacheManager.IsAdd(key))
                {
                    invocation.ReturnValue = _cacheManager.Get(key);
                    return;
                }
                invocation.Proceed();
                _cacheManager.Add(key, invocation.ReturnValue, _duration);
            }

        }

    }
}
