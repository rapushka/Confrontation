using UnityEngine;

namespace Confrontation
{
	public static class LineRendererExtensions
	{
		public static void AddPosition(this LineRenderer @this, Vector2 position)
		{
			@this.positionCount++;
			var index = @this.positionCount - 1;
			@this.SetPosition(index, position);
		}

		public static void ClearPositions(this LineRenderer @this) => @this.positionCount = 0;
	}
}