using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
			GUILayout.Button("Generate field").OnClick(GenerateField);

			GUILayout.BeginHorizontal();
			{
				_height = EditorGUILayout.IntField("Height", _height);
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			{
				_width = EditorGUILayout.IntField(nameof(_width).ToUpper(), _height);
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

	public static class StringExtensions
	{
		public static string Format(this string @this)
		{
			var chars = @this.ToCharArray().ToList();

			if (chars.First() == '_')
			{
				chars.RemoveAt(index: 0);
			}

			chars[0] = char.ToUpper(chars[0]);

			for (var i = 1; i < chars.Count; i++)
			{
				if (char.IsUpper(chars[i]))
				{
					chars.Insert(index: i, item: ' ');
					i++;
				}
			}
			
			return new string(chars.ToArray());
		}
	}
}