using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Confrontation
{
	[CreateAssetMenu(fileName = nameof(BalanceTable), menuName = nameof(Confrontation) + "/" + nameof(BalanceTable))]
	public class BalanceTable : ScriptableObject
	{
		[SerializeField] private List<GoldenMineBalanceData> _goldenMine;
		[SerializeField] private List<BarrackBalanceData> _barrack;

		[field: SerializeField] public UnitBalanceData Unit { get; private set; }

		public LeveledList<GoldenMineBalanceData> GoldenMines { get; private set; }

		public LeveledList<BarrackBalanceData> Barracks { get; private set; }

		private void OnEnable()
		{
			GoldenMines = new LeveledList<GoldenMineBalanceData>(_goldenMine);
			Barracks = new LeveledList<BarrackBalanceData>(_barrack);
		}
	}
}