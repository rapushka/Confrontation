using UnityEngine;

namespace Confrontation
{
	public abstract class Building : MonoBehaviour
	{
		[field: SerializeField] public int OwnerPlayerId { get; set; }
	}
}