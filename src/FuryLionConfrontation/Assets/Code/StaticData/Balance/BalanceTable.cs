using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = nameof(BalanceTable), menuName = nameof(Confrontation) + "/" + nameof(BalanceTable))]
	public class BalanceTable : ScriptableObject
	{
		[field: SerializeField] public LeveledList<BalanceEntry.GoldenMineData> GoldenMines { get; private set; }
	}
}