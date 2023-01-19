using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	public partial class LevelEditorWindow : EditorWindow
	{
		private readonly LevelEditor _levelEditor;
		private int _height;
		private int _width;

		public LevelEditorWindow()
		{
			_levelEditor = new LevelEditor();
		}

		[MenuItem("Tools/Confrontation/LevelEditor")]
		private static void ShowWindow()
		{
			var window = GetWindow<LevelEditorWindow>();
			window.titleContent = new GUIContent(nameof(LevelEditorWindow));
			window.Show();
		}

		// ReSharper disable Unity.PerformanceCriticalCodeInvocation - we don't care about performance in Editor
		private void OnGUI()
		{
			Utils.AsHorizontalGroup(SizesIntFields);

			GUILayout.Button(nameof(GenerateField).Format()).OnClick(GenerateField);

			GUILayout.Button(nameof(Serialize).Format()).OnClick(Serialize);

			// var selected = Selection.activeTransform.gameObject;
		}

		private void GenerateField()
		{
			_levelEditor.GenerateField(_height, _width);
			SaveAll();
		}

		private static void SaveAll()
		{
			foreach (var monoBehaviour in FindObjectsOfType<MonoBehaviour>())
			{
				EditorUtility.SetDirty(monoBehaviour);
			}
		}

		private void SizesIntFields()
		{
			_height = EditorGUILayout.IntField(nameof(_height).Format(), _height);
			_width = EditorGUILayout.IntField(nameof(_width).Format(), _width);
		}

		private void Serialize() => _levelEditor.Serialize();
	}
}