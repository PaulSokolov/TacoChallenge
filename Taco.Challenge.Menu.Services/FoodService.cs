using System;
using Taco.Challenge.Infrastructure;
using Taco.Challenge.Restaurant.Queries;
using Taco.Challenge.Restaurant.Responses;

namespace Taco.Challenge.Restaurant.Services
{
    public class FoodService : IQueryProvider<FoodQuery, FoodQueryResponse>
    {
        public FoodQueryResponse Execute(FoodQuery query)
        {
            dosm ();
            return new FoodQueryResponse { };
        }

        private void dosm() { }
    }
}
