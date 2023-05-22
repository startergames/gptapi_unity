
using Newtonsoft.Json;

namespace unity_gpt_api.Runtime.Options {
    public interface IGptOption {
        [JsonIgnore]
        string URL { get; }
    }
}