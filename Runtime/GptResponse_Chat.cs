using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;

namespace unity_gpt_api.Runtime.Requests {
    public class GptResponse_Chat : IGptResponse {
        public class ChoiceData
        {
            public class MessageData
            {
                [JsonProperty("role")]
                public string Role { get; set; }

                [JsonProperty("content")]
                public string Content { get; set; }
            }
            
            [JsonProperty("index")]
            public int Index { get; set; }

            [JsonProperty("message")]
            public MessageData Message { get; set; }

            [JsonProperty("finish_reason")]
            public string FinishReason { get; set; }
        }
        
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("choices")]
        public List<ChoiceData> Choices { get; set; }

        [JsonProperty("usage")]
        public GptResponse_UsageData Usage { get; set; }
    }
}