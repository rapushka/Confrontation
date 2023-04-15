using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	[CustomEditor(typeof(SpellScriptableObject))]
	public class SpellEditor : UnityEditor.Editor
	{
		private SerializedProperty _durationProperty;
		private SerializedProperty _imageProperty;
		private SerializedProperty _spellTypeProperty;

		private SpellType SpellType => (SpellType)_spellTypeProperty.enumValueIndex;

		[CanBeNull] private Sprite Image => _imageProperty.objectReferenceValue as Sprite;

		private void OnEnable()
		{
			_durationProperty = serializedObject.FindProperty(nameof(ISpell.Duration).AsField());
			_imageProperty = serializedObject.FindProperty(nameof(ISpell.Icon).AsField());
			_spellTypeProperty = serializedObject.FindProperty(nameof(ISpell.SpellType).AsField());
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
			DrawSpellTypeChoice();
			DrawDuration();
		}

		private void DrawIconWithPreview()
		{
			EditorGUILayout.PropertyField(_imageProperty, new GUIContent(text: "Image:"));

			if (Image is not null)
			{
				Image.DrawPreview();
			}
		}

		private void DrawSpellTypeChoice() => EditorGUILayoutTools.SelectionGrid(_spellTypeProperty);

		private void DrawDuration()
		{
			if (SpellType is SpellType.Temporary)
			{
				EditorGUILayout.PropertyField(_durationProperty);
			}
		}
	}
}