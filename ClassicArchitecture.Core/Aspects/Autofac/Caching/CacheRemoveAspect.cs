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
    public class CacheRemoveAspect : MethodInterception
    {
        private readonly string _pattern;
        private readonly ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern = null)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }


    }
}
