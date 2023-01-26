using UnityEngine;
using Zenject;

namespace Confrontation
{
	public abstract class Building : MonoBehaviour
	{
		[field: SerializeField] public int OwnerPlayerId { get; set; }

		public class Factory : PlaceholderFactory<Component, int, Building>
		{
			public T Create<T>(Component parent, int ownerId)
				where T : Building
				=> (T)base.Create(parent, ownerId);
		}
	}
}