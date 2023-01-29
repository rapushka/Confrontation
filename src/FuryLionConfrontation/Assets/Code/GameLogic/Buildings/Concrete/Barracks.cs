using UnityEngine;

namespace Confrontation
{
	public class Barracks : Building
	{
		[SerializeField] private int _productionSpeed;
		[field: SerializeField] public UnitsSquad UnitPrefab { get; private set; }
	}
}