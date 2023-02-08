using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor.Code.Editor
{
	public abstract class IntPairPropertyDrawerBase : PropertyDrawer
	{
		protected virtual int Spacing => 5;
		protected virtual int CountOfElements => 4;

		protected abstract string NameFirst { get; }

		protected abstract string NameSecond { get; }

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position.width = EqualWithForEachElement(position);
			var step = position.width + Spacing;

			EditorGUI.BeginProperty(position, label, property);
			{
				var firstProperty = property.FindPropertyRelative(NameFirst);
				var secondProperty = property.FindPropertyRelative(NameSecond);

				DrawProperty(ref position, NameFirst.Pretty(), step, firstProperty);

				position.x += step;

				DrawProperty(ref position, NameSecond.Pretty(), step, secondProperty);
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

		private float EqualWithForEachElement(Rect position)
			=> position.width / CountOfElements - Spacing * CountOfElements;
	}
}