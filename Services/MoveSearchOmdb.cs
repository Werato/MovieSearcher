using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieSearcher.SharedModels;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace MovieSearcher.Services
{
    public class MoveSearchOmdb : IMoveSearchOmdb
    {
        private readonly HttpClient _client;
        private readonly string _apikey;
        private readonly ILogger _iLogger;
        public MoveSearchOmdb(HttpClient client, IConfiguration conf, ILogger iLogger)
        {
            _client = client;
            _apikey = conf["apikey"];
            _iLogger = iLogger;
        }

        public async Task<MovieDto> SearchByTitleAsync(string title)
        {
            string json = string.Empty;
            var response = await _client.GetAsync($"?apikey={_apikey}&t={title}");
            if (!response.IsSuccessStatusCode)
            {
                _iLogger.LogError(response.Headers.ToString());
                throw new Exception("Movie didn't found");
            }
            json = await response.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<MovieDto>(json);
            return res;
        }
    }
}
