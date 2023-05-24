using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
using Unity.Plastic.Newtonsoft.Json;
#else
using Newtonsoft.Json;
#endif
using unity_gpt_api.Runtime.Requests;
using UnityEngine;

namespace unity_gpt_api.Runtime {
    public class GptApi {
        private static readonly HttpClient client = new HttpClient();
        private string apiKey;
        private GptApiSettings _settings;
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            StringEscapeHandling = StringEscapeHandling.EscapeNonAscii,
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

        [ItemCanBeNull]
        [CanBeNull]
        public async Task<GptResponse_Model> GetModels(CancellationToken cancellationToken = default) {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            if (!string.IsNullOrEmpty(_settings.organizationId)) {
                client.DefaultRequestHeaders.Add("OpenAI-Organization", _settings.organizationId);
            }

            var response = await client.GetAsync("https://api.openai.com/v1/models", cancellationToken);

            if (!response.IsSuccessStatusCode) {
                throw new Exception($"GPT-3 API call failed with status code: {response.StatusCode}");
            }

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GptResponse_Model>(result);
        }

        public async Task<GptResponse_Completion> CompletionAsync(string prompt, int maxTokens = 60) {
            var request = new GptRequest_Completion
            {
                Model = "text-davinci-003",
                Prompt = prompt,
                MaxTokens = maxTokens
            };

            return await RequestAsync(request) as GptResponse_Completion;
        }

        public async Task<IGptResponse> RequestAsync<T>(T request) where T : IGptRequest {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            if (!string.IsNullOrEmpty(_settings.organizationId)) {
                client.DefaultRequestHeaders.Add("OpenAI-Organization", _settings.organizationId);
            }

            var json = JsonConvert.SerializeObject(request, _serializerSettings);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(request.URL, data);

            if (!response.IsSuccessStatusCode) {
                throw new Exception($"GPT-3 API call failed with status code: {response.StatusCode}");
            }

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(result, request.ResponseType) as IGptResponse;
        }
    }
}