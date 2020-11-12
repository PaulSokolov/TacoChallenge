using System;
using System.Collections.Generic;
using System.Text;

namespace Taco.Challenge.Infrastructure
{
    public interface IQuery
    {

    }

    public interface IQuery<TResponse> : IQuery where TResponse : IResponse
    {

    }
}
