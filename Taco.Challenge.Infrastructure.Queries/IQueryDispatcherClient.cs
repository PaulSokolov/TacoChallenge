using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Taco.Challenge.Infrastructure
{
	public interface IQueryDispatcherClient
	{
		TResponse Query<TResponse>(IQuery<TResponse> query) where TResponse : IResponse;

		Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query) where TResponse : IResponse;
	}
}
