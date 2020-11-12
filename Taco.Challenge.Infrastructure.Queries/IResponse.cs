using System;

namespace Taco.Challenge.Infrastructure
{
    public interface IResponse
    {

    }

    public interface IResponse<T> : IResponse
    {
        T Value { get; set; }
    }
}
