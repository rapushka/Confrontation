using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	public class LevelEditorWindow : EditorWindow
	{
		private int _height;
		private int _width;

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
			GUILayout.Button("Generate field").OnClick(GenerateField);
			_height = int.Parse(GUILayout.TextField("Height"));
			_width = int.Parse(GUILayout.TextField("Width"));
		}

		private void GenerateField()
		{
			var cellPrefab = Resources.Load<Cell>("Prefabs/Cell");
			var field = new Field(cellPrefab, _height, _width);
			field.GenerateField();
		}
	}
}