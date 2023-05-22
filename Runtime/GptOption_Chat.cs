using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;

namespace unity_gpt_api.Runtime.Options {
    public class GptOption_ChatMessage
    {
        [JsonProperty("role")]
        public string Role { get; set; }  // system, user, or assistant

        [JsonProperty("content")]
        public string Content { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; } //The name of the author of this message. May contain a-z, A-Z, 0-9, and underscores, with a maximum length of 64 characters.
    }
    
    public class ChatOptions
    {
        [JsonProperty("model")]
        public string Model { get; set; }  // ID of the model to use.

        [JsonProperty("messages")]
        public GptOption_ChatMessage[] Messages { get; set; }  // List of messages describing the conversation so far.

        [JsonProperty("temperature")]
        public float Temperature { get; set; } = 1f;  // Determines output randomness. Higher value for more randomness, lower value for more determinism.

        [JsonProperty("top_p")]
        public float TopP { get; set; } = 1f;  // Nucleus sampling parameter. Lower values will limit output to a smaller vocabulary set.

        [JsonProperty("n")]
        public int N { get; set; } = 1;  // Number of chat completion choices to generate for each input message.

        [JsonProperty("stream")]
        public bool Stream { get; set; } = false;  // If set, partial message deltas will be sent.

        [JsonProperty("stop")]
        public string[] Stop { get; set; }  // Sequences where the API will stop generating further tokens.

        [JsonProperty("max_tokens")]
        public int? MaxTokens { get; set; }  // Maximum number of tokens to generate in the chat completion.

        [JsonProperty("presence_penalty")]
        public float PresencePenalty { get; set; } = 0;  // Penalizes new tokens based on whether they appear in the text so far, encouraging the model to talk about new topics.

        [JsonProperty("frequency_penalty")]
        public float FrequencyPenalty { get; set; } = 0;  // Penalizes new tokens based on their existing frequency in the text so far, discouraging the model from repeating the same line.

        [JsonProperty("logit_bias")]
        public Dictionary<string, float> LogitBias { get; set; }  // Modifies the likelihood of specified tokens appearing in the completion.

        [JsonProperty("user")]
        public string User { get; set; }  // A unique identifier representing your end-user, to help OpenAI monitor and detect abuse.
    }
}