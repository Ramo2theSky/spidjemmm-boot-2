using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Furari.Editor {

    [CustomPropertyDrawer(typeof(SceneReference), true)]
    public class SceneReferencePropertyDrawer : PropertyDrawer {

        private enum SceneBundlingState {
            Nowhere,
            DisabledInBuild,
            EnabledInBuild
        }

        private SerializedProperty sceneReferenceSerializedProperty;
        private SerializedProperty assetSerializedProperty;
        private SerializedProperty pathSerializedProperty;

        private UnityEngine.Object asset;
        private string path;
        private EditorBuildSettingsScene buildEntry;
        private SceneBundlingState bundlingState;

        private bool Initialize(SerializedProperty property) {

            if (property == null || property == sceneReferenceSerializedProperty) { return false; }

            sceneReferenceSerializedProperty = property;
            assetSerializedProperty = property.FindPropertyRelative(nameof(SceneReference.asset));
            pathSerializedProperty = property.FindPropertyRelative(nameof(SceneReference.path));

            asset = assetSerializedProperty.objectReferenceValue;
            path = pathSerializedProperty.stringValue;
            buildEntry = EditorBuildSettings.scenes.FirstOrDefault(s => s.path == path);
            bundlingState = buildEntry == null ? SceneBundlingState.Nowhere : buildEntry.enabled ? SceneBundlingState.EnabledInBuild : SceneBundlingState.DisabledInBuild;
            return true;
        }

        private void SetWith(UnityEngine.Object newAsset) {
            if (asset == newAsset) { return; }

            assetSerializedProperty.objectReferenceValue = newAsset;
            pathSerializedProperty.stringValue = AssetDatabase.GetAssetPath(newAsset);
            buildEntry = EditorBuildSettings.scenes.FirstOrDefault(s => s.path == path);
            bundlingState = buildEntry == null ? SceneBundlingState.Nowhere : buildEntry.enabled ? SceneBundlingState.EnabledInBuild : SceneBundlingState.DisabledInBuild;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (!Initialize(property)) { return; }

            EditorGUI.BeginProperty(position, GUIContent.none, property);

            var selectorFieldRect = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            selectorFieldRect.height = EditorGUIUtility.singleLineHeight;

            var colorToRestore = GUI.color;
            GUI.color = bundlingState switch {
                SceneBundlingState.Nowhere => Color.red,
                SceneBundlingState.DisabledInBuild => Color.yellow,
                _ => colorToRestore
            };

            var newAsset = EditorGUI.ObjectField(selectorFieldRect, asset, typeof(SceneAsset), false);
            SetWith(newAsset);

            GUI.color = colorToRestore;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}