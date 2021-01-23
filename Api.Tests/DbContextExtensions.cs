using PunsApi.Data;
using System;
using System.Collections.Generic;
using System.Text;
using PunsApi.Models;

namespace PUNS.TESTS
{
    public static class DbContextExtensions
    {
        public static void Seed(this AppDbContext dbContext)
        {
            Player testPlayer = new Player
            {
                Email = "admin@o2.pl",
                Nick = "Janusz997"
            };
            dbContext.Players.Add(testPlayer);
            dbContext.SaveChanges();
        }
    }
}
