using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Palmmedia.ReportGenerator.Core;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace unity_gpt_api.Runtime {
    public class GptApi {
        private static readonly HttpClient client = new HttpClient();
        private string apiKey;
        private GptApiSettings _settings;

        public GptApi() {
            // Load the settings from the resources
            _settings = Resources.Load<GptApiSettings>(GptApiSettings.SettingFileName);

            if (_settings != null) {
                apiKey = _settings.apiKey;
            } else {
                Debug.LogError("Failed to load GPT API settings. Please ensure the settings asset exists at " + GptApiSettings.SettingFileName);
            }
        }

        public async Task<string> GetCompletionAsync(string prompt, int maxTokens = 60)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            if (!string.IsNullOrEmpty(_settings.organizationId)) {
                client.DefaultRequestHeaders.Add("OpenAI-Organization", _settings.organizationId);
            }

            var request = new
            {
                model = "text-davinci-003",
                prompt = prompt,
                max_tokens = maxTokens
            };

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://api.openai.com/v1/completions";
            var response = await client.PostAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                Debug.LogError($"GPT-3 API call failed with status code: {response.StatusCode}");
                return null;
            }
        }
    }
}