using PunsApi.Controllers;
using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;
using PunsApi.Models;
using PunsApi.Data;

namespace PUNS.TESTS
{
    public class PlayersControllerUnitTest : TestServerDependent
    {
        private PlayersController controller;
        private readonly AppDbContext dbContext;
        public PlayersControllerUnitTest(TestServerFixture fixture) : base(fixture)
        {
            dbContext = fixture.GetService<AppDbContext>();
            //DbContextMocker.GetAppDbContext(nameof(PlayersControllerUnitTest));
            controller = new PlayersController(dbContext);
        }
        [Fact]
        public async Task TestGetPlayerAsync()
        {
            Guid playerId = dbContext.Players.FirstOrDefault().Id;
            var response = await controller.GetPlayer(playerId);
            var value = response.Value;
            
            Assert.NotNull(value);
        }
        [Fact]
        public async Task TestPutPlayer()
        {
            Guid playerId = dbContext.Players.FirstOrDefault().Id;
            Player player = dbContext.Players.FirstOrDefault();
            var response = await controller.PutPlayer(playerId,player);
            
            Assert.NotNull(response);
        }
        [Fact]
        public async Task TestPostPlayer()
        {
          
            int PlayerQnt = dbContext.Players.Count();
            Player newPlayer = new Player
            {
                Nick = "Jessica111",
                Email = "jessica@o2.pl"
                

            };
            var response = await controller.PostPlayer(newPlayer);
            
            Assert.True(PlayerQnt + 1 == dbContext.Players.Count());
        }
        [Fact]
        public async Task TestDeletePlayer()
        {
            Guid playerId = dbContext.Players.FirstOrDefault().Id;
            int PlayerQnt = dbContext.Players.Count();
            var response = await controller.DeletePlayer(playerId);
           
            Assert.True(PlayerQnt - 1 == dbContext.Players.Count());
        }
    }
}
