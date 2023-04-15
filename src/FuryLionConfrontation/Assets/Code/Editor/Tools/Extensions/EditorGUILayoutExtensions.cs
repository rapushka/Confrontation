using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	public static class EditorGUILayoutExtensions
	{
		public static T AsObjectField<T>(this T @this, bool allowSceneObjects = true)
			where T : Object
			=> (T)EditorGUILayout.ObjectField(@this, typeof(T), allowSceneObjects);
		
		public static T AsObjectField<T>(this T @this, Rect rect, bool allowSceneObjects = true)
			where T : Object
			=> (T)EditorGUI.ObjectField(rect, @this, typeof(T), allowSceneObjects);
	}
}