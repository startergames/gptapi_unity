using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using unity_gpt_api.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace unity_gpt_api.Editor {
    public class GptApiSettingsEditor : SettingsProvider {
        public static GptApiSettings Settings { get; private set; }

        [SettingsProvider]
        public static SettingsProvider CreateProtobufBuilderSettingsProvider() {
            var provider = new GptApiSettingsEditor("Project/Gpt Api", SettingsScope.Project);
            return provider;
        }

        private string[] _models;
        private SerializedObject _settingsObject;

        private SerializedObject SettingsObject {
            get { return _settingsObject ??= new SerializedObject(Settings); }
        }

        public GptApiSettingsEditor(string path, SettingsScope scopes = SettingsScope.Project, IEnumerable<string> keywords = null) : base(path, scopes, keywords) {
            var gpt = new GptApi();

            //_models = gpt.GetModels().Result.Datas.Select(r => r.Id).ToArray();
        }

        public static void CreateFolderRecursively(string path) {
            var parts = path.Split('/');
            var currentPath = "";

            for (var i = 0; i < parts.Length; i++) {
                currentPath += parts[i];
                if (!AssetDatabase.IsValidFolder(currentPath)) {
                    var parentPath = string.Join("/", parts, 0, i);
                    AssetDatabase.CreateFolder(parentPath, parts[i]);
                }

                currentPath += "/";
            }
        }

        public override void OnActivate(string searchContext, VisualElement rootElement) {
            if (Settings != null && !string.IsNullOrEmpty(AssetDatabase.GetAssetPath(Settings))) return;

            var directoryPath = System.IO.Path.GetDirectoryName(GptApiSettings.SettingsPath);
            if (!AssetDatabase.IsValidFolder(directoryPath)) {
                CreateFolderRecursively(directoryPath);
            }

            Settings = AssetDatabase.LoadAssetAtPath<GptApiSettings>(GptApiSettings.SettingsPath);

            if (Settings != null) return;
            Settings = ScriptableObject.CreateInstance<GptApiSettings>();
            AssetDatabase.CreateAsset(Settings, GptApiSettings.SettingsPath);
            AssetDatabase.SaveAssets();
        }

        public override void OnGUI(string searchContext) {
            EditorGUILayout.PropertyField(SettingsObject.FindProperty(nameof(GptApiSettings.apiKey)));
            EditorGUILayout.PropertyField(SettingsObject.FindProperty(nameof(GptApiSettings.organizationId)));

            if (_models != null) {
                var selectedIndex = _models.ToList().IndexOf(Settings.defaultCompletionModel);
                selectedIndex = EditorGUILayout.Popup(
                    "Default Completion Model",
                    selectedIndex,
                    _models);
                SettingsObject.FindProperty(nameof(GptApiSettings.defaultCompletionModel)).stringValue =
                    selectedIndex >= 0 ? _models[selectedIndex] : "";
            }
            else {
                if (GUILayout.Button("Get Models")) {
                    var gpt = new GptApi();

                    var cts = new CancellationTokenSource();
                    cts.CancelAfter(TimeSpan.FromSeconds(60));

                    var getModelsTask = gpt.GetModels(cts.Token);
                    _models = getModelsTask.Result.Datas.Select(r => r.Id).ToArray();
                }
            }

            SettingsObject.ApplyModifiedProperties();
        }
    }
}