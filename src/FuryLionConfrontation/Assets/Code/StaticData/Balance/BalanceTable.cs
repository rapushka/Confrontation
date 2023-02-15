using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = nameof(BalanceTable), menuName = nameof(Confrontation) + "/" + nameof(BalanceTable))]
	public class BalanceTable : ScriptableObject
	{
		[SerializeField] private BalanceEntry[] _levelsOfProgress;

		public int MaxLevel => _levelsOfProgress.Length;

		public IBalanceEntry GetEntryForLevel(int level) => _levelsOfProgress[level - 1];
	}
}