using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	public class LevelEditorWindow : EditorWindow
	{
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
		}

		private void GenerateField()
		{
			var cellPrefab = Resources.Load<Cell>("Prefabs/Cell");
			var field = new Field(cellPrefab);
			field.Initialize();
		}
	}
}