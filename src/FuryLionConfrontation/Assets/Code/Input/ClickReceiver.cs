using UnityEngine;

namespace Confrontation
{
	[RequireComponent(typeof(Collider))]
	public class ClickReceiver : MonoBehaviour
	{
		[field: SerializeField] public Cell Cell { get; private set; }
	}
}