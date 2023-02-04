using UnityEditor;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class CellsRegionGizmoDrawer : IGuiRenderable
	{
		private static bool _drawGizmosForCellsRegions;

		public void GuiRender()
		{
			_drawGizmosForCellsRegions
				= EditorGUILayout.Toggle(nameof(_drawGizmosForCellsRegions).Pretty(), _drawGizmosForCellsRegions);
		}

		[DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
		private static void DrawGizmoForMyScript(Cell cell, GizmoType gizmoType)
		{
			if (_drawGizmosForCellsRegions == false)
			{
				return;
			}

			if (cell.RelatedRegion == true)
			{
				Gizmos.color = cell.RelatedRegion.OwnerPlayerId switch
				{
					0     => Color.white,
					1     => Color.cyan,
					2     => Color.red,
					3     => Color.yellow,
					var _ => Random.ColorHSV(),
				};
			}

			Gizmos.DrawSphere(cell.transform.position, 0.25f);
		}
	}
}