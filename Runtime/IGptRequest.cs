
// #if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
// using Unity.Plastic.Newtonsoft.Json;
// #else
using Newtonsoft.Json;
//#endif

namespace unity_gpt_api.Runtime.Requests {
    public interface IGptRequest {
        [JsonIgnore]
        string URL { get; }
        
        [JsonIgnore]
        System.Type ResponseType { get; }
    }
}