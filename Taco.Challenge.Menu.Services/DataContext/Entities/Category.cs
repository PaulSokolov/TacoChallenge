using System;
using System.Collections.Generic;
using System.Text;

namespace Taco.Challenge.Restaurant.Services.DataContext.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MenuItem> MenuItems { get; set; }
    }
}
