using UnityEditor;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class CellsRegionGizmoDrawer : IInitializable, IGuiRenderable
	{
		private static bool _drawLineToRegionVillage;
		private static float _verticalOffset = 0.25f;

		private static Vector3 VerticalOffset => Vector3.up * _verticalOffset;

		public void Initialize() => _drawLineToRegionVillage = true;

		public void GuiRender()
		{
			_drawLineToRegionVillage
				= EditorGUILayout.Toggle(nameof(_drawLineToRegionVillage).Pretty(), _drawLineToRegionVillage);

			_verticalOffset = EditorGUILayout.FloatField(nameof(_verticalOffset).Pretty(), _verticalOffset);
		}

		[DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
		private static void DrawGizmoForMyScript(Cell cell, GizmoType gizmoType)
		{
			if (_drawLineToRegionVillage == false
			    || cell.RelatedRegion == false)
			{
				return;
			}

			Gizmos.color = cell.RelatedRegion.OwnerPlayerId switch
			{
				0     => Color.white,
				1     => Color.cyan,
				2     => Color.red,
				3     => Color.yellow,
				var _ => Random.ColorHSV(),
			};

			var village = cell.RelatedRegion.RelatedCell;
			Gizmos.DrawLine(cell.transform.position + VerticalOffset, village.transform.position + VerticalOffset);
		}
	}
}