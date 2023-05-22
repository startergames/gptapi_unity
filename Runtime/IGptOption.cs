
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
using Unity.Plastic.Newtonsoft.Json;
#else
using Newtonsoft.Json;
#endif

namespace unity_gpt_api.Runtime.Options {
    public interface IGptOption {
        [JsonIgnore]
        string URL { get; }
    }
}