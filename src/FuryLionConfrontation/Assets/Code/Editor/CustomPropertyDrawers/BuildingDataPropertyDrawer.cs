using UnityEditor;
using UnityEngine;

namespace Confrontation
{
	[CustomPropertyDrawer(typeof(Building.Data))]
	public class BuildingDataPropertyDrawer : PropertyDrawer
	{
		private static string[] BuildingsNames => BuildingsCollection.BuildingsNames;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position.height = EditorGUIUtility.singleLineHeight;

			EditorGUI.BeginProperty(position, label, property);
			{
				var prefabProperty = property.FindPropertyRelative("_prefab");
				var selectionIndex = property.FindPropertyRelative("_selectionIndex");

				selectionIndex.intValue = EditorGUI.Popup(position, selectionIndex.intValue, BuildingsNames);
				prefabProperty.objectReferenceValue = BuildingsCollection.Load(selectionIndex.intValue);
			}
			EditorGUI.EndProperty();

			property.serializedObject.ApplyModifiedProperties();
		}
	}
}