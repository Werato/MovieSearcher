using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using MovieSearcher.Controllers;
using MovieSearcher.Services;
using MovieSearcher.SharedModels;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    [TestClass]
    public sealed class OmdbMovieServiceTests
    {
        private const string ApiKey = "testkey";
        private readonly Mock<IMoveSearchOmdb> _serviceMock;
        private readonly MoviesController _controller;
        private readonly MockHttpMessageHandler _mockHttp;

        public OmdbMovieServiceTests()
        {
            _mockHttp = new MockHttpMessageHandler();
            _serviceMock = new Mock<IMoveSearchOmdb>();
            _controller = new MoviesController(_serviceMock.Object);
        }

        [TestMethod]
        public async Task Get_ReturnsOk_WhenServiceWork()
        {
            // Arrange
            var title = "Inception";
            var movies = new List<MovieDto>
            {
                new MovieDto { Title = "Inception", imdbID = "tt1375666" }
            };
            // Act
            var result = await _controller.Get(title);

            // Assert
            Assert.IsInstanceOfType<OkObjectResult>(result);
        }

        [TestMethod]
        public async Task Get_ThrowsException_WhenServiceThrows()
        {
            // Arrange
            var title = "Inception";

            // Act & Assert
            Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await _controller.Get(title));
        }

        [TestMethod]
        public async Task SearchByTitleAsync_ThrowsExceptionWithCorrectMessage_WhenResponseNotSuccessful()
        {
            // Arrange
            var title = "UnknownMovie";
            _mockHttp
               .When(HttpMethod.Get, $"http://www.omdbapi.com/?apikey={ApiKey}&t={title}")
               .Respond(HttpStatusCode.NotFound);
            _serviceMock.Setup(s => s.SearchByTitleAsync(title)).ThrowsAsync(new Exception("Movie didn't found"));
            // Act
            var ex = await Assert.ThrowsExceptionAsync<Exception>(async () => await _controller.Get(title));

            // Assert
            Assert.AreEqual("Movie didn't found", ex.Message);
        }
    }
}
