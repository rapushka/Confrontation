using UnityEditor;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class CellsPlayerColorSphereGizmoDrawer : IInitializable, IGuiRenderable
	{
		private const float GizmoRadius = 0.25f;
		private const int IdForRandomColors = -1;

		private static bool _drawGizmosColorOfOwner;

		public void Initialize() => _drawGizmosColorOfOwner = true;

		public void GuiRender()
			=> _drawGizmosColorOfOwner
				= EditorGUILayout.Toggle(nameof(_drawGizmosColorOfOwner).Pretty(), _drawGizmosColorOfOwner);

		[DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
		private static void DrawGizmo(Cell cell, GizmoType gizmoType)
		{
			if (_drawGizmosColorOfOwner == false)
			{
				return;
			}

			var id = cell.RelatedRegion == true ? cell.RelatedRegion.OwnerPlayerId : IdForRandomColors;
			GizmoUtils.SetColorBy(id);

			Gizmos.DrawSphere(cell.transform.position, GizmoRadius);
		}
	}
}