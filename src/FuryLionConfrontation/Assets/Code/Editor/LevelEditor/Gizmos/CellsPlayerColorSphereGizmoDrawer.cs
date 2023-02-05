using UnityEditor;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class CellsPlayerColorSphereGizmoDrawer : IInitializable, IGuiRenderable
	{
		private static bool _drawGizmosColorOfOwner;

		public void Initialize() => _drawGizmosColorOfOwner = true;

		public void GuiRender()
			=> _drawGizmosColorOfOwner
				= EditorGUILayout.Toggle(nameof(_drawGizmosColorOfOwner).Pretty(), _drawGizmosColorOfOwner);

		[DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
		private static void DrawGizmoForMyScript(Cell cell, GizmoType gizmoType)
		{
			if (_drawGizmosColorOfOwner == false)
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