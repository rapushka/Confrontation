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

		[field: SerializeField] public SettlementStats SettlementStats { get; private set; }

		[field: SerializeField] public EnemiesStats EnemiesStats { get; private set; }

		[field: SerializeField] public TimeStats TimeStats { get; private set; }

		public int BuildPriceFor(Building building)
			=> building switch
			{
				Barrack    => BarrackStats.Price,
				GoldenMine => GoldenMineStats.Price,
				var _      => throw new ArgumentException($"There is no price for {building.Name}"),
			};
	}
}