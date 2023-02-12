using UnityEngine;

namespace Confrontation
{
	public static class LineRendererExtensions
	{
		public static void AddPosition(this LineRenderer @this, Vector3 position)
		{
			@this.positionCount++;
			@this.SetLastPosition(position);
		}

		public static void SetLastPosition(this LineRenderer @this, Vector3 position)
		{
			var index = @this.positionCount - 1;
			@this.SetPosition(index, position);
		}

		public static void ClearPositions(this LineRenderer @this) => @this.positionCount = 0;

		public static bool IsDrawing(this LineRenderer @this) => @this.positionCount > 1;
	}
}