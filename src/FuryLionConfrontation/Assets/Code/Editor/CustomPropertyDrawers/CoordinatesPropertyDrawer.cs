using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor.Code.Editor
{
	[CustomPropertyDrawer(typeof(Coordinates))]
	public class CoordinatesPropertyDrawer : PropertyDrawer
	{
		private const string NameRow = "_row"; 
		private const string NameColumn = "_column";
		private SerializedProperty _content;

		private const int Spacing = 5;
		private const int CountOfElements = 4;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position.width = EqualWithForEachElement(position);
			var step = position.width + Spacing;

			EditorGUI.BeginProperty(position, label, property);
			{
				var rowProperty = property.FindPropertyRelative(NameRow);
				var columnProperty = property.FindPropertyRelative(NameColumn);

				DrawProperty(ref position, NameRow.Pretty(), step, rowProperty);

				position.x += step;

				DrawProperty(ref position, NameColumn.Pretty(), step, columnProperty);
			}
			EditorGUI.EndProperty();

			property.serializedObject.ApplyModifiedProperties();
		}

		private static void DrawProperty(ref Rect rect, string label, float spacing, SerializedProperty property)
		{
			EditorGUI.LabelField(rect, label);
			rect.x += spacing;
			EditorGUI.PropertyField(rect, property, GUIContent.none);
		}

		private static float EqualWithForEachElement(Rect position)
			=> position.width / CountOfElements - Spacing * CountOfElements;
	}
}