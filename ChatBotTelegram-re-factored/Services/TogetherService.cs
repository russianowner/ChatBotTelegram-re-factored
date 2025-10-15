using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ChatBotTelegram_re_factored.Services
{
    public class TogetherService
    {
        private readonly string _apiKey;
        private const string TogetherUrl = "https://api.together.xyz/v1/chat/completions";

        public TogetherService(string apiKey)
        {
            _apiKey = apiKey;
        }
        public async Task<string?> GetReplyAsync(string prompt)
        {
            try
            {
                using var http = new HttpClient();
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                var payload = new
                {
                    model = "meta-llama/Llama-3.3-70B-Instruct-Turbo-Free",
                    messages = new[] { new { role = "user", content = prompt } },
                    temperature = 0.8,
                    max_tokens = 1000
                };
                var json = JsonSerializer.Serialize(payload);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await http.PostAsync(TogetherUrl, content);
                var body = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                using var doc = JsonDocument.Parse(body);
                if (doc.RootElement.TryGetProperty("choices", out var choices) &&
                    choices.GetArrayLength() > 0 &&
                    choices[0].TryGetProperty("message", out var message) &&
                    message.TryGetProperty("content", out var contentProp))
                {
                    return contentProp.GetString()?.Trim();
                }
                return null;
            }
            catch (JsonException jex)
            {
                Console.WriteLine($"{jex.Message}");
                return null;
            }
            catch (HttpRequestException hex)
            {
                Console.WriteLine($"{hex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return null;
            }
        }
    }
}
