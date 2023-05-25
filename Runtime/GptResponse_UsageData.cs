
// #if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
// using Unity.Plastic.Newtonsoft.Json;
// #else
using Newtonsoft.Json;
//#endif

namespace unity_gpt_api.Runtime.Requests {
    public class GptResponse_UsageData
    {
        [JsonProperty("prompt_tokens")]
        public int PromptTokens { get; set; }

        [JsonProperty("completion_tokens")]
        public int CompletionTokens { get; set; }

        [JsonProperty("total_tokens")]
        public int TotalTokens { get; set; }
    }
}