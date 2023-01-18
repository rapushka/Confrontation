using System.Globalization;
using System.Text;
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
			GUILayout.Button(nameof(GenerateField).Format()).OnClick(GenerateField);

			GUILayout.BeginHorizontal();
			{
				_height = EditorGUILayout.IntField(nameof(_height).Format(), _height);
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			{
				_width = EditorGUILayout.IntField(nameof(_width).Format(), _width);
			}
			GUILayout.EndHorizontal();
		}

		private void GenerateField()
		{
			var cellPrefab = Resources.Load<Cell>("Prefabs/Cell");
			var field = new Field(cellPrefab, _height, _width);
			field.GenerateField();
		}
	}
}