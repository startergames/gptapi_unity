using System.Collections.Generic;

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
using Unity.Plastic.Newtonsoft.Json;
#else
using Newtonsoft.Json;
#endif

namespace unity_gpt_api.Runtime.Requests {
    public class GptResponse_Model : IGptResponse {
        public class Data {
            public class PermissionData {
                [JsonProperty("id")]
                public string Id { get; set; }
                [JsonProperty("object")]
                public string Object { get; set; }
                [JsonProperty("created")]
                public long Created { get; set; }
                [JsonProperty("allow_create_engine")]
                public bool AllowCreateEngine { get; set; }
                [JsonProperty("allow_sampling")]
                public bool AllowSampling { get; set; }
                [JsonProperty("allow_logprobs")]
                public bool AllowLogprobs { get; set; }
                [JsonProperty("allow_search_indices")]
                public bool AllowSearchIndices { get; set; }
                [JsonProperty("allow_view")]
                public bool AllowView { get; set; }
                [JsonProperty("allow_fine_tuning")]
                public bool AllowFineTuning { get; set; }
                [JsonProperty("organization")]
                public string Organization { get; set; }
                [JsonProperty("group")]
                public string Group { get; set; }
                [JsonProperty("is_blocking")]
                public bool IsBlocking { get; set; }
                
            }
            [JsonProperty("id")]
            public string Id { get; set; }
        
            [JsonProperty("object")]
            public string Object { get; set; }
            
            [JsonProperty("created")]
            public long Created { get; set; }
        
            [JsonProperty("owned_by")]
            public string OwnedBy { get; set; }
        
            [JsonProperty("permission")]
            public List<PermissionData> Permission { get; set; }
            
            [JsonProperty("root")]
            public string Root { get; set; }
        }
        
        [JsonProperty("data")]
        public List<Data> Datas { get; set; }
        
        [JsonProperty("object")]
        public string Object { get; set; }
    }
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