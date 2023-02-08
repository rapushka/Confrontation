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
				(nameof(Village), "Prefabs/Buildings/Village"),
				(nameof(GoldenMine), "Prefabs/Buildings/Golden Mine"),
				(nameof(Barracks), "Prefabs/Buildings/Barracks"),
				(nameof(Capital), "Prefabs/Buildings/Capital"),
			};

		private string[] BuildingsNames => _buildings.Select((t) => t.Name).ToArray();

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position.height = EditorGUIUtility.singleLineHeight;

			EditorGUI.BeginProperty(position, label, property);
			{
				var prefabProperty = property.FindPropertyRelative("_prefab");
				var coordinatesProperty = property.FindPropertyRelative("_coordinates");
				var selectionIndex = property.FindPropertyRelative("_selectionIndex");

				selectionIndex.intValue = EditorGUI.Popup(position, selectionIndex.intValue, BuildingsNames);

				prefabProperty.objectReferenceValue = Resources.Load(_buildings[selectionIndex.intValue].Path);

				position.y += EditorGUIUtility.singleLineHeight;

				EditorGUI.PropertyField(position, coordinatesProperty, GUIContent.none);
			}
			EditorGUI.EndProperty();

			property.serializedObject.ApplyModifiedProperties();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
			=> base.GetPropertyHeight(property, label) * 2 + 10;
	}
}