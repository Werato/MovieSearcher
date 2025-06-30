using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Xunit;

namespace Tests
{
    [TestClass]
    public sealed class OmdbMovieServiceTests
    {
        private const string ApiKey = "testkey";
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly OmdbMovieService _service;

        public OmdbMovieServiceTests()
        {
            _mockHttp = new MockHttpMessageHandler();
            var client = _mockHttp.ToHttpClient();
            client.BaseAddress = new Uri("http://www.omdbapi.com");
            var options = Options.Create(new OmdbSettings { ApiKey = ApiKey });
            _service = new OmdbMovieService(client, options);
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
