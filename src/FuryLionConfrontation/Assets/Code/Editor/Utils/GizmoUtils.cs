using UnityEngine;

namespace Confrontation.Editor
{
	public static class GizmoUtils
	{
		public static void SetColorBy(int id)
			=> Gizmos.color = id switch
			{
				0     => Color.white,
				1     => Color.cyan,
				2     => Color.red,
				3     => Color.yellow,
				var _ => Random.ColorHSV(),
			};
	}
}