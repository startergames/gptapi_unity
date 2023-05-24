using UnityEngine;

namespace unity_gpt_api.Runtime {
    public class GptApiSettings : ScriptableObject {
        public const string SettingFileName = "GptApiSettings";
        public const string SettingsDirectory = "Assets/GptApi/Resources/";
        public const string SettingsPath = SettingsDirectory + SettingFileName + ".asset";

        public string apiKey = "";
        public string organizationId = "";
        public string defaultCompletionModel = "";
    }
}