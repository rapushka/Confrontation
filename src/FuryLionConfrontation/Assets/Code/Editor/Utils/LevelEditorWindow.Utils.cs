using System;
using UnityEditor;

namespace Confrontation.Editor
{
	public partial class LevelEditorWindow
	{
		private static class Utils
		{
			public static void AsHorizontalGroup(Action @this)
			{
				EditorGUILayout.BeginHorizontal();
				@this.Invoke();
				EditorGUILayout.EndHorizontal();
			}
		}
	}
}