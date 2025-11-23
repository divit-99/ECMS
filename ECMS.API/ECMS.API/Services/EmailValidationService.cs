using ECMS.API.Models;
using ECMS.API.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace ECMS.API.Services
{
    public class EmailValidationService : IEmailValidationService
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;
        private readonly IMemoryCache _cache;

        public EmailValidationService(HttpClient client, IConfiguration config, IMemoryCache cache)
        {
            _client = client;
            _apiKey = config["AbstractApi:ApiKey"];
            _cache = cache;
        }

        public async Task<bool> IsValidAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string key = $"email-validity:{email}";

            if (_cache.TryGetValue(key, out bool cachedResult))
                return cachedResult;

            var url = $"https://emailreputation.abstractapi.com/v1/?" +
                $"api_key={_apiKey}&email={email}&auto_correct=false";

            var httpResponse = await _client.GetAsync(url);
            var raw = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Abstract API error: {raw}");
            }

            var response = JsonSerializer.Deserialize<AbstractApiResponse>(raw);
            bool isValid = string.Equals(response?.Email_Deliverability?.Status, "deliverable", StringComparison.OrdinalIgnoreCase);

            _cache.Set(key, isValid, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(9) });

            return isValid;
        }
    }
}
