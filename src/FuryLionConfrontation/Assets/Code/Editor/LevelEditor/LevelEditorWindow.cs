using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	public partial class LevelEditorWindow : EditorWindow
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
			Utils.AsHorizontalGroup(SizesIntFields);

			GUILayout.Button(nameof(GenerateField).Format()).OnClick(GenerateField);
		}

		private void SizesIntFields()
		{
			_height = EditorGUILayout.IntField(nameof(_height).Format(), _height);
			_width = EditorGUILayout.IntField(nameof(_width).Format(), _width);
		}

		private void GenerateField()
		{
			var cellPrefab = Resources.Load<Cell>("Prefabs/Cell");
			var field = new Field(cellPrefab, _height, _width);
			field.GenerateField();
		}
	}
}