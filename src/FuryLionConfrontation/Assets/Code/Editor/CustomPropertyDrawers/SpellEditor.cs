using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	[CustomEditor(typeof(SpellScriptableObject))]
	public class SpellEditor : UnityEditor.Editor
	{
		private SerializedProperty _titleProperty;
		private SerializedProperty _descriptionProperty;
		private SerializedProperty _iconProperty;
		private SerializedProperty _spellTypeProperty;
		private SerializedProperty _durationProperty;
		private SerializedProperty _manaCoastProperty;

		private SerializedObject SerializedObject => serializedObject;

		private SpellType SpellType => (SpellType)_spellTypeProperty.enumValueIndex;

		[CanBeNull] private Sprite Image => _iconProperty.objectReferenceValue as Sprite;

		private void OnEnable()
		{
			_titleProperty = SerializedObject.FindProperty(nameof(ISpell.Title).AsField());
			_descriptionProperty = SerializedObject.FindProperty(nameof(ISpell.Description).AsField());
			_durationProperty = SerializedObject.FindProperty(nameof(ISpell.Duration).AsField());
			_iconProperty = SerializedObject.FindProperty(nameof(ISpell.Icon).AsField());
			_spellTypeProperty = SerializedObject.FindProperty(nameof(ISpell.SpellType).AsField());
			_manaCoastProperty = SerializedObject.FindProperty(nameof(ISpell.ManaCoast).AsField());
		}

		public override void OnInspectorGUI()
		{
			SerializedObject.Update();
			DrawFields();
			SerializedObject.ApplyModifiedProperties();
		}

		private void DrawFields()
		{
			EditorGUILayoutTools.Header("Info");
			DrawTitle();
			DrawDescription();
			DrawIconWithPreview();

			EditorGUILayoutTools.Header("Balance");
			DrawSpellTypeChoice();
			DrawDuration();
			DrawManaCoast();
		}

		private void DrawTitle() => EditorGUILayout.PropertyField(_titleProperty);

		private void DrawDescription()
			=> EditorGUILayoutTools.TextArea(_descriptionProperty, nameof(ISpell.Description), minRowsCount: 3);

		private void DrawIconWithPreview()
		{
			EditorGUILayout.PropertyField(_iconProperty, new GUIContent(text: nameof(ISpell.Icon)));

			// ReSharper disable once UseNullPropagation — is derived from Unity.Object
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
		private void DrawManaCoast() => EditorGUILayout.PropertyField(_manaCoastProperty);
	}
}