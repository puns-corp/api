using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;

namespace PUNS.TESTS
{
    public class TestServerDependent : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;
        public TestServer TestServer => _fixture.Server;
        public HttpClient Client => _fixture.Client;

        public TestServerDependent(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        protected TService GetService<TService>()
            where TService : class
        {
            return _fixture.GetService<TService>();
        }
    }
}
