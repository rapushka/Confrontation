using UnityEngine;

namespace Confrontation
{
	public abstract class Building : MonoBehaviour
	{
		[field: SerializeField] public Cell RelatedCell { get; set; }
	}
}