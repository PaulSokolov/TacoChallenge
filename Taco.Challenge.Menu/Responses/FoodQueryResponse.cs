using System;
using System.Collections.Generic;
using System.Text;
using Taco.Challenge.Infrastructure;

namespace Taco.Challenge.Restaurant.Responses
{
    public class FoodQueryResponse : IItemsResponse<object>
    {
        public object[] Items { get; set; }
    }
}
