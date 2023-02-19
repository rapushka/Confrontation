using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = nameof(BalanceTable), menuName = nameof(Confrontation) + "/" + nameof(BalanceTable))]
	public class BalanceTable : ScriptableObject, IBalanceTable
	{
		[field: SerializeField] public UnitStats Unit { get; private set; }

		[field: SerializeField] public GoldenMineStats GoldenMine { get; private set; }

		[field: SerializeField] public BarrackStats Barrack { get; private set; }

		[field: SerializeField] public VillageStats Village { get; private set; }
	}
}