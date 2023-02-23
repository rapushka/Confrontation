using System;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = nameof(BalanceTable), menuName = nameof(Confrontation) + "/" + nameof(BalanceTable))]
	public class BalanceTable : ScriptableObject, IBalanceTable
	{
		[field: SerializeField] public UnitStats UnitStats { get; private set; }

		[field: SerializeField] public GoldenMineStats GoldenMineStats { get; private set; }

		[field: SerializeField] public BarrackStats BarrackStats { get; private set; }

		[field: SerializeField] public VillageStats VillageStats { get; private set; }

		public int PriceFor(Building building)
			=> building switch
			{
				Barrack   => BarrackStats.Price,
				GoldenMine => GoldenMineStats.Price,
				var _      => throw new ArgumentException($"There is no price for {building.GetType().Name}"),
			};
	}
}