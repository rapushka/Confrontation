using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = nameof(BalanceTable), menuName = nameof(Confrontation) + "/" + nameof(BalanceTable))]
	public class BalanceTable : ScriptableObject, IBalanceTable
	{
		[SerializeField] private List<GoldenMineBalanceData> _goldenMine;
		[SerializeField] private List<BarrackBalanceData> _barrack;
		[SerializeField] private List<VillageBalanceData> _village;

		[field: SerializeField] public UnitBalanceData Unit { get; private set; }

		public LeveledList<GoldenMineBalanceData> GoldenMine { get; private set; }

		public LeveledList<BarrackBalanceData> Barrack { get; private set; }

		public LeveledList<VillageBalanceData> Village { get; private set; }

		private void OnEnable()
		{
			GoldenMine = new LeveledList<GoldenMineBalanceData>(_goldenMine);
			Barrack = new LeveledList<BarrackBalanceData>(_barrack);
			Village = new LeveledList<VillageBalanceData>(_village);
		}
	}
}