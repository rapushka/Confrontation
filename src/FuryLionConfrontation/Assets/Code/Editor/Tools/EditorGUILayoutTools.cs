using System;
using UnityEditor;

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
	}
}