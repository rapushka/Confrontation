using UnityEditor;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class CellsRegionVillageLineGizmoDrawer : IInitializable, IGuiRenderable
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

			GizmoUtils.SetColorBy(cell.RelatedRegion.OwnerPlayerId);

			var village = cell.RelatedRegion.RelatedCell;
			Gizmos.DrawLine(cell.transform.position + VerticalOffset, village.transform.position + VerticalOffset);
		}
	}
}