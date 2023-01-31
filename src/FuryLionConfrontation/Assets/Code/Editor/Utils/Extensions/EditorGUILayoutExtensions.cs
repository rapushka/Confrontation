using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	public static class EditorGUILayoutExtensions
	{
		public static T AsObjectField<T>(this T @this, bool allowSceneObjects = true)
			where T : Object
			=> (T)EditorGUILayout.ObjectField(@this, typeof(T), allowSceneObjects);
	}
}