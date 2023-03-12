using UnityEngine;

namespace Confrontation
{
	public class EditorFieldBounds : IFieldBounds
	{
		public bool IsInBounds(Vector2 position, float maxDeviation) => true;
	}
}