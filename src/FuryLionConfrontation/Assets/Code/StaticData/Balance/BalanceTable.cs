using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = nameof(BalanceTable), menuName = nameof(Confrontation) + nameof(BalanceTable))]
	public class BalanceTable : ScriptableObject
	{
		[SerializeField] private BalanceEntry[] _entries;

		public IBalanceEntry GetEntryForLevel(int level) => _entries[level];
	}
}