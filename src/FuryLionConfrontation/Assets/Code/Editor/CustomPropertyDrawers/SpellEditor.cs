using System;
using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	[CustomEditor(typeof(SpellScriptableObject))]
	public class SpellEditor : UnityEditor.Editor
	{
		private SerializedProperty _isPermanent;
		private SerializedProperty _duration;
		private SerializedProperty _imageProperty;

		private SpellType _spellType;

		private SpellScriptableObject Target => (SpellScriptableObject)target;

		private static GUIContent GuiImage => new("Image:");

		private void OnEnable()
		{
			_isPermanent = serializedObject.FindProperty(nameof(ISpell.SpellType).AsField());
			_duration = serializedObject.FindProperty(nameof(ISpell.Duration).AsField());
			_imageProperty = serializedObject.FindProperty(nameof(ISpell.Icon).AsField());
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			DrawFields();
			serializedObject.ApplyModifiedProperties();
		}

		private void DrawFields()
		{
			DrawIconWithPreview();
			DrawIsPermanentFlag();
			DrawDuration();

			var options = Enum.GetNames(typeof(SpellType));
			_spellType = (SpellType)GUILayout.SelectionGrid((int)_spellType, options, xCount: 1);
		}

		private void DrawIconWithPreview()
		{
			EditorGUILayout.PropertyField(_imageProperty, GuiImage);

			if (_imageProperty.objectReferenceValue is Sprite sprite)
			{
				sprite.DrawPreview();
				Target.Icon = sprite;
			}
		}

		private void DrawIsPermanentFlag() => EditorGUILayout.PropertyField(_isPermanent);

		private void DrawDuration()
		{
			if (_isPermanent.boolValue == false)
			{
				EditorGUILayout.PropertyField(_duration);
			}
		}
	}
}