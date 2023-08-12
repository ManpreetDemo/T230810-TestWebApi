using Newtonsoft.Json;
using TestWebApi230809.Model;

namespace TestWebApi230809.Services
{
    public class StoryApi : IStoryApi
    {

        //private string BaseApiUrl = "https://hacker-news.firebaseio.com/v0";
        private string BaseApiUrl = "";
        public StoryApi(IConfiguration configuration)
        {
            //var uriString = configuration["BaseApiUrl"];
            var uriString = configuration["BaseApiUrl"];
            BaseApiUrl = uriString;
        }
        public async Task<List<int>> GetBestStoryIds()
        {
            var storyIds = new List<int>();
            using (var httpClient = new HttpClient())
            {
                string bestStoriesUrl = $"{BaseApiUrl}/beststories.json";
                string bestStoryIds = await httpClient.GetStringAsync(bestStoriesUrl);
                storyIds = JsonConvert.DeserializeObject<List<int>>(bestStoryIds);                
            }
            return storyIds;
        }
        public async Task<Story> GetStory(HttpClient httpClient, int storyId)
        {
            var storyUrl = $"{BaseApiUrl}/item/{storyId}.json";
            var storyJson = await httpClient.GetStringAsync(storyUrl);
            var story = JsonConvert.DeserializeObject<Story>(storyJson);
            return story;
        }
    }
}
