using System;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	[Serializable]
	public abstract class Building : MonoBehaviour
	{
		public Data StaticData { get; set; } = new();
		
		[Serializable]
		public class Data
		{
			[field: SerializeField] public int         OwnerPlayerId { get; set; }
			[field: SerializeField] public Coordinates Coordinates   { get; private set; }
		}

		public class Factory : PlaceholderFactory<Building, Transform, int, Building>
		{
			public T Create<T>(Building prefab, Transform ownerCell, int ownerId)
				where T : Building
				=> (T)base.Create(prefab, ownerCell, ownerId);
		}
	}
}