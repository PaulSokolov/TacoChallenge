using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using Taco.Challenge.Restaurant.Services.DataContext.Entities;

namespace Taco.Challenge.Restaurant.Services.DataContext
{
    public class Context: DbContext
    {
        public DbSet<Entities.Restaurant> Restaurants { get; set; }
        private string _connection;
        public Context(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("RestaurantDB");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection);
        }

    }
}
