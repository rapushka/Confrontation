using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	[CustomEditor(typeof(SpellScriptableObject))]
	public class SpellEditor : UnityEditor.Editor
	{
		private SerializedProperty _isPermanent;
		private SerializedProperty _duration;

		private SpellScriptableObject Target => (SpellScriptableObject)target;

		private void OnEnable()
		{
			_isPermanent = serializedObject.FindProperty(nameof(ISpell.IsPermanent).AsField());
			_duration = serializedObject.FindProperty(nameof(ISpell.Duration).AsField());
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			DrawFields();
			serializedObject.ApplyModifiedProperties();
		}

		private void DrawFields()
		{
			DrawIcon();

			EditorGUILayout.PropertyField(_isPermanent);

			if (_isPermanent.boolValue == false)
			{
				EditorGUILayout.PropertyField(_duration);
			}
		}

		private void DrawIcon()
		{
			var imageProperty = serializedObject.FindProperty("_icon");
			EditorGUILayout.PropertyField(imageProperty, new GUIContent("Image"));

			if (imageProperty.objectReferenceValue is Sprite sprite)
			{
				sprite.DrawPreview();
				Target.Icon = sprite;
			}
		}

	}
}