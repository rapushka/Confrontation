using System;
using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	public static class EditorGUILayoutTools
	{
		public static void AsHorizontalGroup(Action elements)
		{
			EditorGUILayout.BeginHorizontal();
			elements.Invoke();
			EditorGUILayout.EndHorizontal();
		}

		public static void AsHorizontalGroup(params Action[] elements)
		{
			EditorGUILayout.BeginHorizontal();

			foreach (var element in elements)
			{
				element.Invoke();
			}

			EditorGUILayout.EndHorizontal();
		}

		public static void SelectionGrid(SerializedProperty property, int columnsCount = 1)
		{
			var selectedValue = property.enumValueIndex;
			var selectedIndex = GUILayout.SelectionGrid(selectedValue, property.enumNames, columnsCount);
			property.enumValueIndex = selectedIndex;
		}

		public static void TextArea(SerializedProperty property, string label, int minRowsCount = 1)
		{
			EditorGUILayout.LabelField(label);
			var minHeight = CalculateRowsWithSpacingsFor(minRowsCount);
			property.stringValue = EditorGUILayout.TextArea(property.stringValue, GUILayout.MinHeight(minHeight));
		}

		public static void Header(string text)
		{
			var style = new GUIStyle(EditorStyles.boldLabel)
			{
				fontSize = 16,
			};
			GUILayout.Space(pixels: EditorGUIUtility.singleLineHeight);
			GUILayout.Label(text, style, GUILayout.ExpandWidth(true));
		}

		private static float CalculateRowsWithSpacingsFor(int rowsCount)
			=> CalculateHeightFor(rowsCount) + CalculateSpacingsFor(rowsCount);

		private static float CalculateHeightFor(int rowsCount) => EditorGUIUtility.singleLineHeight * rowsCount;

		private static float CalculateSpacingsFor(int rowsCount)
			=> EditorGUIUtility.standardVerticalSpacing * (rowsCount - 1);
	}
}