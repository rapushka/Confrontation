using UnityEngine;
using Zenject;

namespace Confrontation
{
	public abstract class Building : MonoBehaviour
	{
		public int  OwnerPlayerId { get; set; }
		public Cell RelatedCell   { get; set; }

		public class Factory : PlaceholderFactory<Building, Transform, int, Building>
		{
			public T Create<T>(Building prefab, Transform ownerCell, int ownerId)
				where T : Building
				=> (T)base.Create(prefab, ownerCell, ownerId);
		}
	}
}