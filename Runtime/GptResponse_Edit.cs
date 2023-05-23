using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;

namespace unity_gpt_api.Runtime.Requests {
    public class GptResponse_Edit : IGptResponse {
        public class ChoiceData
        {
            [JsonProperty("text")]
            public string Text { get; set; }

            [JsonProperty("index")]
            public int Index { get; set; }
        }

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