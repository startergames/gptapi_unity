using UnityEngine;

namespace unity_gpt_api.Runtime {
    public class GptApiSettings : ScriptableObject {
        public const string SettingFileName = "GptApiSettings";
        public const string SettingsPath = "Assets/GptApi/Resources/" + SettingFileName + ".asset";
        
        public string apiKey = "";
        public string organizationId = "";
    }
}