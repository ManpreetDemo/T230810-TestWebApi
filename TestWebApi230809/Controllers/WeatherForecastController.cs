//using Microsoft.AspNetCore.Mvc;
//using System.Linq.Expressions;

//namespace TestWebApi230809.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class WeatherForecastController : ControllerBase
//    {
//        private static readonly string[] Summaries = new[]
//        {
//            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//        };

//        private readonly ILogger<WeatherForecastController> _logger;

//        public WeatherForecastController(ILogger<WeatherForecastController> logger)
//        {
//            _logger = logger;
//        }

//        [HttpGet(Name = "GetWeatherForecast")]
//        public IEnumerable<WeatherForecast> Get()
//        {
//            LogInfo("GetWeatherForecast");
//            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//            {
//                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                TemperatureC = Random.Shared.Next(-20, 55),
//                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//            })
//            .ToArray();
//        }
//        private void LogInfo(string text) => _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}]: {text}");
//    }


//public async Task<ActionResult<IEnumerable<Story>>> Get(int n)
//{
//    this.LogInfo($"Received a request for {n} best stories.");
//    try
//    {
//        HttpClient httpClient = _httpClientFactory.CreateClient();
//        string bestStoriesUrl = $"{BaseApiUrl}/beststories.json";
//        string bestStoryIds = await httpClient.GetStringAsync(bestStoriesUrl);
//        var storyIds = JsonConvert.DeserializeObject<List<int>>(bestStoryIds);
//        if (storyIds == null)
//        {
//            this.LogError("Failed to retrieve best story IDs from the API.");
//            return BadRequest("Failed to retrieve story IDs.");
//        }
//        var stories = new List<Story>();
//        var tasks = new List<Task>();
//        for (int i = 0; i < Math.Min(n, storyIds.Count); i++)
//        {
//            var storyUrl = $"https://hacker-news.firebaseio.com/v0/item/{storyIds[i]}.json";
//            var task = FetchStoryDetails(httpClient, storyUrl, stories);
//            tasks.Add(task);
//        }
//        await Task.WhenAll(tasks);
//        stories.Sort((a, b) => b.Score.CompareTo(a.Score));
//        this.LogInfo($"Returning the best stories response.");
//        return Ok(stories);
//    }
//    catch (JsonException ex)
//    {
//        this.LogError("Failed to parse JSON response from the API.", ex);
//        return BadRequest("Failed to parse JSON response from the API.");
//    }
//    catch (HttpRequestException ex)
//    {
//        this.LogError("Failed to make a request to the API.", ex);
//        return BadRequest("Failed to retrieve data from the API.");
//    }
//    catch (Exception ex)
//    {
//        var exText = "An error occurred while processing the request.";
//        _logger.LogError(ex, "An error occurred while processing the request.");
//        //return StatusCode(500, exText);
//        return BadRequest(ex.Message);
//    }
//}
//private async Task FetchStoryDetails(HttpClient httpClient, string storyUrl, List<Story> stories)
//{
//    try
//    {
//        var storyJson = await httpClient.GetStringAsync(storyUrl);
//        var story = JsonConvert.DeserializeObject<Story>(storyJson);
//        if (story != null) stories.Add(story);
//    }
//    catch (JsonException ex)
//    {
//        this.LogError($"Error deserializing story JSON: {ex.Message}", ex);
//    }
//    catch (Exception ex)
//    {
//        this.LogError($"An error occurred while fetching story details: {ex.Message}", ex);
//    }
//}
//}

