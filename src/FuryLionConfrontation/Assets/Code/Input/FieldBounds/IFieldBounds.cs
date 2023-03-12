using UnityEngine;

namespace Confrontation
{
	public interface IFieldBounds
	{
		bool IsInBounds(Vector2 position, float maxDeviation);
	}
}