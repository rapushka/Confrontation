using UnityEngine;

namespace Confrontation
{
	public class BordersContainer : MonoBehaviour
	{
		[field: SerializeField] public Border Left        { get; private set; }
		[field: SerializeField] public Border LeftTop     { get; private set; }
		[field: SerializeField] public Border RightTop    { get; private set; }
		[field: SerializeField] public Border Right       { get; private set; }
		[field: SerializeField] public Border RightBottom { get; private set; }
		[field: SerializeField] public Border LeftBottom  { get; private set; }
	}
}