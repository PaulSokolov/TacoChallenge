using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Taco.Challenge.Infrastructure;
using Taco.Challenge.Restaurant.Queries;
using Taco.Challenge.Restaurant.Responses;
using Taco.Challenge.Restaurant.Services.DataContext;

namespace Taco.Challenge.Restaurant.Services
{
    public class FoodService : IQueryAsyncProvider<FoodQuery, FoodQueryResponse>
    {
        private readonly IConfiguration _configuration;

        public FoodService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public Task<FoodQueryResponse> Execute(FoodQuery query)
        {
            var searchString = query.SearchText;
            using (var context = new Context(_configuration))
            {
                var r = context.Restaurants.ToList();
                if (string.IsNullOrEmpty(searchString))
                    return Task.FromResult(new FoodQueryResponse());

                var searchWords = searchString.Split(' ');

                //search string should contain at least City or Suburb and Category or MenuItem name
                var searchResult = context.Restaurants
                    .Where(_ => (searchString.Contains(_.City) || searchString.Contains(_.Suburb)))
                    .Where(_ => _.Categories.Any(c => searchString.Contains(c.Name)) || _.Categories.Any(c => c.MenuItems.Any(mi => searchWords.Any(word => mi.Name.Contains(word)))));

                return Task.FromResult(new FoodQueryResponse());

            }
        }

    }
}
