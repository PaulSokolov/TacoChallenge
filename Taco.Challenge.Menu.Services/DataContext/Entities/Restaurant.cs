using System;
using System.Collections.Generic;
using System.Text;

namespace Taco.Challenge.Restaurant.Services.DataContext.Entities
{
    public class Restaurant 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }
        public string LogoPath { get; set; }
        public int Rank { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
