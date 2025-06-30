using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Options;
using MovieSearcher.SharedModels;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace MovieSearcher.Services
{
    public class MoveSearchOmdb : IMoveSearchOmdb
    {
        private readonly HttpClient _client;
        private readonly string _apikey;
        public MoveSearchOmdb(HttpClient client, IConfiguration conf)
        {
            _client = client;
            _apikey = conf["apikey"];
        }

        public async Task<MovieDto> SearchByTitleAsync(string title)
        {
            string json = string.Empty;
            var response = await _client.GetAsync($"?apikey={_apikey}&t={title}");
            if (!response.IsSuccessStatusCode)
            {
                return null;//TODO: add exeption add logging
            }
            json = await response.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<MovieDto>(json);
            return res;
        }
    }
}
