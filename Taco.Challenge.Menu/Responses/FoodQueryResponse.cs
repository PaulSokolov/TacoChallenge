using System;
using System.Collections.Generic;
using System.Text;
using Taco.Challenge.Infrastructure;

namespace Taco.Challenge.Restaurant.Responses
{
    public class FoodQueryResponse : IItemsResponse<Restaurant>
    {
        public Restaurant[] Items { get; set; } = new Restaurant[0];
    }

    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }
        public IEnumerable<MenuItem> MenuItems { get; set; }
    }

    public class Restaurant
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }
        public string LogoPath { get; set; }
        public int Rank { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
