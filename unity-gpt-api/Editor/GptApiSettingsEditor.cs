using System.Collections.Generic;
using unity_gpt_api.Runtime;
using UnityEditor;
using UnityEngine;

namespace unity_gpt_api.Editor {
    public class GptApiSettingsEditor : SettingsProvider {
        public static GptApiSettings Settings { get; private set; }

        [InitializeOnLoadMethod]
        private static void Initialize() {
            if (Settings != null) return;
            
            var directoryPath = System.IO.Path.GetDirectoryName(GptApiSettings.SettingsPath);
            if (!AssetDatabase.IsValidFolder(directoryPath))
            {
                AssetDatabase.CreateFolder(System.IO.Path.GetDirectoryName(directoryPath), System.IO.Path.GetFileName(directoryPath));
            }
            Settings = AssetDatabase.LoadAssetAtPath<GptApiSettings>(GptApiSettings.SettingsPath);
            
            if (Settings != null) return;
            Settings = ScriptableObject.CreateInstance<GptApiSettings>();
            AssetDatabase.CreateAsset(Settings, GptApiSettings.SettingsPath);
            AssetDatabase.SaveAssets();
        }
        
        [SettingsProvider]
        public static SettingsProvider CreateProtobufBuilderSettingsProvider() {
            var provider = new GptApiSettingsEditor("Project/Gpt Api", SettingsScope.Project);
            return provider;
        }

        private SerializedObject _settingsObject;
        private SerializedObject SettingsObject {
            get {
                return _settingsObject ??= new SerializedObject(Settings);
            }
        }

	    public GptApiSettingsEditor(string path, SettingsScope scopes = SettingsScope.Project, IEnumerable<string> keywords = null) : base(path, scopes, keywords) {
        }

        public override void OnGUI(string searchContext) {
            EditorGUILayout.PropertyField(SettingsObject.FindProperty(nameof(GptApiSettings.apiKey)));
        }
    }
}