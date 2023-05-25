using System.Collections.Generic;

// #if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
// using Unity.Plastic.Newtonsoft.Json;
// #else
using Newtonsoft.Json;
//#endif

namespace unity_gpt_api.Runtime.Requests {
    public class GptResponse_Completion : IGptResponse {
        public class ChoiceData
        {
            [JsonProperty("text")]
            public string Text { get; set; }

            [JsonProperty("index")]
            public int Index { get; set; }

            [JsonProperty("logprobs")]
            public object Logprobs { get; set; }

            [JsonProperty("finish_reason")]
            public string FinishReason { get; set; }
        }
        
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("choices")]
        public List<ChoiceData> Choices { get; set; }

        [JsonProperty("usage")]
        public GptResponse_UsageData Usage { get; set; }
    }
}