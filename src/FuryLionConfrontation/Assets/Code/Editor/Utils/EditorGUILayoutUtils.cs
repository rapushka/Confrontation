using System;
using UnityEditor;

namespace Confrontation.Editor
{
	public static class EditorGUILayoutUtils
	{
		public static void AsHorizontalGroup(Action elements)
		{
			EditorGUILayout.BeginHorizontal();
			elements.Invoke();
			EditorGUILayout.EndHorizontal();
		}
	}
}