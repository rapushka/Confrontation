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
			if (_drawGizmosForCellsRegions)
			{
				Gizmos.DrawSphere(cell.transform.position, 0.25f);
			}
		}
	}
}