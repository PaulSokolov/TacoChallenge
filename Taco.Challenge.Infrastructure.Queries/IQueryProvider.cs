using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Taco.Challenge.Infrastructure
{
    public interface IQueryProvider
    {

    }

    public interface IQueryProvider<in TQuery, out TResponse> : IQueryProvider
        where TResponse : IResponse
        where TQuery : IQuery<TResponse>
    {
        TResponse Execute(TQuery query);
    }

    public interface IQueryAsyncProvider<in TQuery, TResponse> : IQueryProvider
        where TResponse : IResponse
        where TQuery : IQuery<TResponse>
    {
        Task<TResponse> Execute(TQuery query);
    }
}
