using Blazored.LocalStorage;

namespace DataBase
{
    public class SearchHistory
    {
        private const string Key = "searchHistory"; //todo: appsettings
        private readonly ILocalStorageService _storage;
        private List<string> _history = new();
        public SearchHistory(ILocalStorageService localStorageService)
        {
            _storage = localStorageService;

        }
        public async Task InitializeAsync()
        {
            _history.Clear();
            var list = await _storage.GetItemAsync<List<string>>(Key);
            _history = list ?? new List<string>();
        }
        public async Task AddAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return;
            _history.Insert(0, query);
            if (_history.Count > 5)
                _history = _history.Take(5).ToList();

            await _storage.SetItemAsync(Key, _history);
        }
        public List<string> GetHistory() => _history;
    }
}
