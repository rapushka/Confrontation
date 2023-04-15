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
	}
}