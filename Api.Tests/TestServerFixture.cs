using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PunsAPI;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PUNS.TESTS
{
    public class TestServerFixture : IDisposable
    {
        public TestServer Server { get; }
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var hostBuilder = CreateWebHostBuilder();

            //new WebHostBuilder()
            //.UseEnvironment("Testing")

            //.UseStartup<Startup>();



            //var hostBuilder = WebHost.CreateDefaultBuilder()
            //.ConfigureServices(service=>service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>())
            //.UseEnvironment("IntegrationTest")
            //.UseStartup<Startup>();
            Server = new TestServer(hostBuilder);
            Client = Server.CreateClient();
        }
        public static IWebHostBuilder CreateWebHostBuilder() =>
        WebHost.CreateDefaultBuilder()
        
            //.ConfigureTestServices(services =>
            //{
                   
            //    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //    services.AddHttpContextAccessor();
            //    services.Configure<CookiePolicyOptions>(options =>
            //    {
            //        options.CheckConsentNeeded = context => true;
            //        options.MinimumSameSitePolicy = SameSiteMode.None;
            //    });

            //})
        
            .UseEnvironment("IntegrationTest")
            .UseStartup<Startup>();

        
        public void Dispose()
        {
            Server.Dispose();
            Client.Dispose();
        }

        public TService GetService<TService>()
            where TService : class
        {
            return Server?.Host?.Services?.GetService(typeof(TService)) as TService;
        }
    }
}
