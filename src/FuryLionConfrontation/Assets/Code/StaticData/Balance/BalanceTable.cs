using System;
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = nameof(BalanceTable), menuName = nameof(Confrontation) + "/" + nameof(BalanceTable))]
	public class BalanceTable : ScriptableObject
	{
		[SerializeField] private List<GoldenMineBalanceData> _goldenMineData;
		[SerializeField] private List<BarrackBalanceData> _barrackData;

		[field: SerializeField] public float BaseUnitsSpeed { get; private set; }

		public LeveledList<GoldenMineBalanceData> GoldenMines { get; private set; }

		public LeveledList<BarrackBalanceData> Barracks { get; private set; }

		private void OnEnable()
		{
			GoldenMines = new LeveledList<GoldenMineBalanceData>(_goldenMineData);
			Barracks = new LeveledList<BarrackBalanceData>(_barrackData);
		}
	}
}