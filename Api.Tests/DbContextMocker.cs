using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PunsApi.Data;
using PunsApi.Hubs;
using PunsApi.Hubs.Interfaces;
using PunsApi.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUNS.TESTS
{
    public static class DbContextMocker
    {
        public static AppDbContext GetAppDbContext(string dbName)
        {
            
        // Create options for DbContext instance
           var options = new DbContextOptionsBuilder<AppDbContext>()
                //.UseInMemoryDatabase(databaseName: dbName)
                .Options;
            // Create instance of DbContext
            var dbContext = new AppDbContext(options);

            // Add entities in memory
            dbContext.Seed();

            return dbContext;
        }
    }
}
