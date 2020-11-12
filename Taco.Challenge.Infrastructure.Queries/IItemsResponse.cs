using System;
using System.Collections.Generic;
using System.Text;

namespace Taco.Challenge.Infrastructure
{
    public interface IItemsResponse<TItem> : IResponse
    {
        TItem[] Items { get; set; }
    }
}
