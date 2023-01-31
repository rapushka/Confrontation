using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	public class LevelEditorWindow : EditorWindow
	{
		private readonly LevelEditor _levelEditor;
		private int _height;
		private int _width;

		public LevelEditorWindow() => _levelEditor = new LevelEditor();

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
			EditorGUILayoutUtils.AsHorizontalGroup(SizesIntFields);

			GUILayout.Button(nameof(GenerateField).Format()).OnClick(GenerateField);
			GUILayout.Button(nameof(Serialize).Format()).OnClick(Serialize);
			GUILayout.Button(nameof(SelectionToVillage).Format()).OnClick(SelectionToVillage);
			GUILayout.Button(nameof(_levelEditor.UpdateField).Format()).OnClick(_levelEditor.UpdateField);
		}

		private void SizesIntFields()
		{
			_height = EditorGUILayout.IntField(nameof(_height).Format(), _height);
			_width = EditorGUILayout.IntField(nameof(_width).Format(), _width);
		}

		private void GenerateField()
		{
			_levelEditor.GenerateField(_height, _width);
			SaveAll();
		}

		private void Serialize() => Debug.Log(_levelEditor.Serialize());

		private void SelectionToVillage()
		{
			var selected = Selection.gameObjects.First();
			_levelEditor.ToVillage(selected);
		}

		private static void SaveAll()
		{
			foreach (var monoBehaviour in FindObjectsOfType<MonoBehaviour>())
			{
				EditorUtility.SetDirty(monoBehaviour);
			}
		}
	}
}