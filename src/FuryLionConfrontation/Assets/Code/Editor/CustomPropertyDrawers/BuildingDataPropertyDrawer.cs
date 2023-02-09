using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Confrontation
{
	[CustomPropertyDrawer(typeof(Building.Data))]
	public class BuildingDataPropertyDrawer : PropertyDrawer
	{
		private const int VerticalSpacing = 5;

		private readonly List<(string Name, string Path)> _buildings
			= new()
			{
				(nameof(Village), Constants.ResourcePath.Village),
				(nameof(GoldenMine), Constants.ResourcePath.GoldenMine),
				(nameof(Barracks), Constants.ResourcePath.Barracks),
				(nameof(Capital), Constants.ResourcePath.Capital),
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
			=> base.GetPropertyHeight(property, label) * 2 + VerticalSpacing;
	}
}