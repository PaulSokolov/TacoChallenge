using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Taco.Challenge.Infrastructure.InMemory
{
    public class QueryDispatcherClient : IQueryDispatcherClient
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcherClient(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }
        public TResponse Query<TResponse>(IQuery<TResponse> query) where TResponse : IResponse
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            List<Assembly> allAssemblies = new List<Assembly>();

            foreach (string dll in Directory.GetFiles(path, "Taco.*.dll"))
                allAssemblies.Add(Assembly.LoadFile(dll));

            var queryExecutorInterface = typeof(IQueryProvider<,>).MakeGenericType(query.GetType(), typeof(TResponse));
            
            var types = allAssemblies.SelectMany(s => s.GetTypes()).Where(p => queryExecutorInterface.IsAssignableFrom(p));

            var serviceType = types.First();
            var actionToInvoke = serviceType.GetMethods()
                .Where(m => m.Name == "Execute" && m.GetParameters().Any(p => p.ParameterType == query.GetType()))
                .FirstOrDefault();

            var parameters = new List<object>();
            bool isMatched = true;
            foreach (var constructor in serviceType.GetConstructors().OrderByDescending(_ => _.GetParameters().Count()))
            {
                foreach (var parameterType in constructor.GetParameters().Select(_ => _.ParameterType))
                {
                    var param = _serviceProvider.GetService(parameterType);
                    if (param == null)
                    {
                        isMatched = false;
                        parameters = new List<object>();
                        break;
                    }

                    parameters.Add(param);
                }

                if (isMatched)
                {
                    var service = Activator.CreateInstance(serviceType, parameters.ToArray());

                    return (TResponse)actionToInvoke.Invoke(service, new[] { query });
                }
            }

            throw new Exception("There is no handlers for this query");
        }

        public Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query) where TResponse : IResponse
        {
            return Task.FromResult(Query(query));
        }
    }
}
