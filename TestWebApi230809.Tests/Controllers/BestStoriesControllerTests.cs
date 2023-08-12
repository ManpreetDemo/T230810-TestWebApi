using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using TestWebApi230809.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace TestWebApi230809.Controllers.Tests
{
    [TestClass()]
    public class BestStoriesControllerTests
    {
        private readonly TestServer _server;
        private static WebApplicationFactory<Startup> _factory;
        private HttpClient _client;
        private BestStoriesController _controller;
        private Mock<ILogger<BestStoriesController>> _mockLogger = new Mock<ILogger<BestStoriesController>>();
        private Mock<IStoryService> _mockStoryService = new Mock<IStoryService>();
        private Mock<IStoryApi> _mockStoryApi = new Mock<IStoryApi>();
        public BestStoriesControllerTests()
        {
            var builder = new WebHostBuilder()
                              .UseStartup<Startup>()
                               .ConfigureTestServices(services =>
                               {
                                   // Register the mock IStoryApi implementation
                                   services.AddScoped<IStoryApi>(_ => _mockStoryApi.Object);
                               });
            _server = new TestServer(builder);
            _client = _server.CreateClient();
            _controller = new BestStoriesController(_mockLogger.Object,_mockStoryService.Object);
            //var sAuthority = HttpContext.Current.Request.Url.Authority;
        }
        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
      
        [TestMethod]
        public async Task Get_ReturnsBestNStories()
        {
            // Arrange
            //var client = _factory.CreateClient();
            //client.BaseAddress = BaseUri;
            var n = 37052586; // Specify the desired 'n' value

            // Act
            var response1 = await _controller.Get(2);
            var response = await _client.GetAsync($"/api/BestStories/{n}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var stories = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(stories);
        }
    }
}