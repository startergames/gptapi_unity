using System.Collections.Generic;

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
using Unity.Plastic.Newtonsoft.Json;
#else
using Newtonsoft.Json;
#endif

namespace unity_gpt_api.Runtime.Options {
    public class GptOption_Completion : IGptOption {
        public string URL => "https://api.openai.com/v1/completions";
        
        // ID of the model to use. The model ID changes the underlying AI model used for text generation. Different models have different capabilities.
        [JsonProperty("model", DefaultValueHandling = DefaultValueHandling.Include)]
        public string Model { get; set; } = "text-davinci-002";
        
        // The prompt(s) to generate completions for, encoded as a string. This is the starting text that the AI will generate from.
        [JsonProperty("prompt")]
        public string Prompt { get; set; }
        
        // The suffix that comes after a completion of inserted text. This can be used to add a specific ending to all generated text.
        [JsonProperty("suffix")]
        public string Suffix { get; set; }
        
        // The maximum number of tokens to generate in the completion. The larger this number, the longer the generated text.
        [JsonProperty("max_tokens")]
        public int MaxTokens { get; set; } = 60;
        
        // What sampling temperature to use, between 0 and 2. Higher values will make the output more random. Lower values make it more deterministic.
        [JsonProperty("temperature")]
        public double Temperature { get; set; } = 0.5;
        
        // An alternative to sampling with temperature, called nucleus sampling. Lower values make the text more focused and deterministic. Higher values make it more diverse.
        [JsonProperty("top_p")]
        public double TopP { get; set; } = 1;
        
        // How many completions to generate for each prompt. More completions give more variety, but consume more resources.
        [JsonProperty("n")]
        public int N { get; set; } = 1;
        
        // Whether to stream back partial progress. If true, the API will return results as they are generated, rather than all at once at the end.
        [JsonProperty("stream")]
        public bool Stream { get; set; } = false;
        
        // Include the log probabilities on the logprobs most likely tokens. If set, the API will return additional information about the likelihood of each token.
        [JsonProperty("logprobs")]
        public int? Logprobs { get; set; } = null;
        
        // Echo back the prompt in addition to the completion. If true, the API will return the prompt in addition to the generated text.
        [JsonProperty("echo")]
        public bool Echo { get; set; } = false;
        
        // Up to 4 sequences where the API will stop generating further tokens. The returned text will not contain the stop sequence.
        [JsonProperty("stop")]
        public string[] Stop { get; set; }
        
        // Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they appear in the text so far. This can be used to encourage the model to talk about new topics.
        [JsonProperty("presence_penalty")]
        public double PresencePenalty { get; set; } = 0;
        
        // Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency in the text so far. This can be used to discourage the model from repeating itself.
        [JsonProperty("frequency_penalty")]
        public double FrequencyPenalty { get; set; } = 0;
        
        // Generates best_of completions server-side and returns the "best" (the one with the highest log probability per token). The model will generate multiple completions and return the most probable one.
        [JsonProperty("best_of")] public int BestOf { get; set; } = 1;
        
        // Modify the likelihood of specified tokens appearing in the completion. This can be used to bias the model towards or against specific tokens.
        [JsonProperty("logit_bias")]
        public Dictionary<string, double> LogitBias { get; set; } = new Dictionary<string, double>();

        // A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
        [JsonProperty("user")]
        public string User { get; set; }
    }
}