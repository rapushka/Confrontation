using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Confrontation
{
	[CustomPropertyDrawer(typeof(Building.Data))]
	public class BuildingDataPropertyDrawer : PropertyDrawer
	{
		private readonly List<(string Name, string Path)> _buildings
			= new()
			{
				(nameof(Settlement), Constants.ResourcePath.Settlement),
				(nameof(GoldenMine), Constants.ResourcePath.GoldenMine),
				(nameof(Barrack), Constants.ResourcePath.Barrack),
				(nameof(Capital), Constants.ResourcePath.Capital),
			};

		private string[] BuildingsNames => _buildings.Select((t) => t.Name).ToArray();

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position.height = EditorGUIUtility.singleLineHeight;

			EditorGUI.BeginProperty(position, label, property);
			{
				var prefabProperty = property.FindPropertyRelative("_prefab");
				var selectionIndex = property.FindPropertyRelative("_selectionIndex");

				selectionIndex.intValue = EditorGUI.Popup(position, selectionIndex.intValue, BuildingsNames);
				prefabProperty.objectReferenceValue = Resources.Load(_buildings[selectionIndex.intValue].Path);
			}
			EditorGUI.EndProperty();

			property.serializedObject.ApplyModifiedProperties();
		}
	}
}