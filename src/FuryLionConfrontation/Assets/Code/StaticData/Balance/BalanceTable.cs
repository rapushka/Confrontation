using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = nameof(BalanceTable), menuName = nameof(Confrontation) + "/" + nameof(BalanceTable))]
	public class BalanceTable : ScriptableObject, IBalanceTable
	{
		[SerializeField] private List<GoldenMineStats> _goldenMine;
		[SerializeField] private List<BarrackStats> _barrack;
		[SerializeField] private List<VillageStats> _village;

		[field: SerializeField] public UnitStats Unit { get; private set; }

		public LeveledList<GoldenMineStats> GoldenMine { get; private set; }

		public LeveledList<BarrackStats> Barrack { get; private set; }

		public LeveledList<VillageStats> Village { get; private set; }

		private void OnEnable()
		{
			GoldenMine = new LeveledList<GoldenMineStats>(_goldenMine);
			Barrack = new LeveledList<BarrackStats>(_barrack);
			Village = new LeveledList<VillageStats>(_village);
		}
	}
}