using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
using Unity.Plastic.Newtonsoft.Json;
#else
using Newtonsoft.Json;
#endif
using unity_gpt_api.Runtime.Options;
using UnityEngine;

namespace unity_gpt_api.Runtime {
    public class GptApi {
        private static readonly HttpClient client = new HttpClient();
        private string apiKey;
        private GptApiSettings _settings;
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };

        public GptApi() {
            // Load the settings from the resources
            _settings = Resources.Load<GptApiSettings>(GptApiSettings.SettingFileName);

            if (_settings != null) {
                apiKey = _settings.apiKey;
            } else {
                Debug.LogError("Failed to load GPT API settings. Please ensure the settings asset exists at " + GptApiSettings.SettingFileName);
            }
        }

        public async Task<string> CompletionAsync(string prompt, int maxTokens = 60) {
            var request = new GptOption_Completion
            {
                Model = "text-davinci-003",
                Prompt = prompt,
                MaxTokens = maxTokens
            };

            return await RequestAsync(request);
        }

        public async Task<string> RequestAsync<T>(T request) where T : IGptOption {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            if (!string.IsNullOrEmpty(_settings.organizationId)) {
                client.DefaultRequestHeaders.Add("OpenAI-Organization", _settings.organizationId);
            }

            var json = JsonConvert.SerializeObject(request, _serializerSettings);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(request.URL, data);

            if (!response.IsSuccessStatusCode) {
                Debug.LogError($"GPT-3 API call failed with status code: {response.StatusCode}");
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}