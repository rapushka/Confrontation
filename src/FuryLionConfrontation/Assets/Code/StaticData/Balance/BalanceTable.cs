using System;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = nameof(BalanceTable), menuName = nameof(Confrontation) + "/" + nameof(BalanceTable))]
	public class BalanceTable : ScriptableObject, IBalanceTable
	{
		[field: SerializeField] public UnitStats UnitStats { get; private set; }

		[field: SerializeField] public GoldenMineStats GoldenMineStats { get; private set; }

		[field: SerializeField] public TowerOfMagesStats TowerOfMagesStats { get; private set; }

		[field: SerializeField] public BarrackStats BarrackStats { get; private set; }

		[field: SerializeField] public SettlementStats SettlementStats { get; private set; }

		[field: SerializeField] public ForgeStats ForgeStats { get; private set; }

		[field: SerializeField] public FortStats FortStats { get; private set; }

		[field: SerializeField] public QuarryStats QuarryStats { get; private set; }

		[field: SerializeField] public WorkshopStats WorkshopStats { get; private set; }

		[field: SerializeField] public FarmStats FarmStats { get; private set; }

		[field: SerializeField] public StableStats StableStats { get; private set; }

		[field: SerializeField] public EnemiesStats EnemiesStats { get; private set; }

		[field: SerializeField] public TimeStats TimeStats { get; private set; }

		public int BuildPriceFor(Building building)
			=> building switch
			{
				Barrack      => BarrackStats.Price,
				GoldenMine   => GoldenMineStats.Price,
				Farm         => FarmStats.Price,
				Stable       => StableStats.Price,
				Forge        => ForgeStats.Price,
				Fort         => FortStats.Price,
				Quarry       => QuarryStats.Price,
				Workshop     => WorkshopStats.Price,
				TowerOfMages => TowerOfMagesStats.Price,
				var _        => throw new ArgumentException($"There is no price for {building.Name}"),
			};
	}
}