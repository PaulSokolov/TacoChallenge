using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taco.Challenge.Infrastructure;
using Taco.Challenge.Restaurant.Exceptions;
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
        public async Task<FoodQueryResponse> Execute(FoodQuery query)
        {
            var searchString = query.SearchText;
            using (var context = new Context(_configuration))
            {
                if (string.IsNullOrEmpty(searchString))
                    return new FoodQueryResponse();

                var searchWords = searchString.Split(' ');

                //search string should contain at least City or Suburb and Category or MenuItem name
                var searchResrtaurants = await context.Restaurants
                    .Include(_ => _.Categories)
                    .ThenInclude(_ => _.MenuItems)
                    .Where(_ => (searchString.Contains(_.City) || searchString.Contains(_.Suburb))).ToListAsync();

                if (!searchResrtaurants.Any())
                    throw new RestaurantsNotFoundException();

                var searchResult = searchResrtaurants.Where(_ => _.Categories.Any(c => searchString.Contains(c.Name)) || _.Categories.Any(c => c.MenuItems.Any(mi => searchWords.Any(word => mi.Name.Contains(word)))));
                
                var result = new List<Taco.Challenge.Restaurant.Responses.Restaurant>();
                foreach (var item in searchResult)
                {
                    result.Add(new Responses.Restaurant
                    {
                        Name = item.Name,
                        Rank = item.Rank,
                        City = item.City,
                        Suburb = item.Suburb,
                        LogoPath = item.LogoPath,
                        Categories = item.Categories.Where(_ => searchString.Contains(_.Name) || _.MenuItems.Any(mi => searchWords.Any(word => mi.Name.Contains(word))))
                            .Select(_ => new Category
                            {
                                Name = _.Name,
                                MenuItems = _.MenuItems.Where(mi => searchWords.Any(word => mi.Name.Contains(word))).Select(_ => new MenuItem { Id = _.Id, Name = _.Name, Price = _.Price }).ToList()
                            })
                            .ToList()
                    });

                }
                return new FoodQueryResponse
                {
                    Items = result.ToArray()
                };
            }
        }

    }
}
