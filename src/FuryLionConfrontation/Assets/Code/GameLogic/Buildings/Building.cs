using UnityEngine;
using Zenject;

namespace Confrontation
{
	public abstract class Building : MonoBehaviour, ICoordinated
	{
		public int  OwnerPlayerId { get; set; }
		public Cell RelatedCell   { get; set; }

		public Coordinates Coordinates { get; set; }

		public class Factory : PlaceholderFactory<Building, Building>
		{
			public T Create<T>(T prefab, Cell ownerCell, int ownerId)
				where T : Building
			{
				var building = Create(prefab, ownerCell.transform, ownerId);
				building.RelatedCell = ownerCell;
				return building;
			}

			public T Create<T>(T prefab, Transform ownerCell, int ownerId)
				where T : Building
			{
				var building = base.Create(prefab);
				building.transform.SetParent(ownerCell, worldPositionStays: false);
				building.OwnerPlayerId = ownerId;
				return (T)building;
			}

			public Building Create(Building prefab, Cell cell)
			{
				var building = Create(prefab, cell.transform, cell.RelatedRegion.OwnerPlayerId);
				cell.Building = building;
				building.RelatedCell = cell;
				return building;
			}

			public T Create<T>(Building prefab)
				where T : Building
				=> (T)base.Create(prefab);
		}
	}
}