using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = nameof(BalanceTable), menuName = nameof(Confrontation) + "/" + nameof(BalanceTable))]
	public class BalanceTable : ScriptableObject
	{
		[SerializeField] private List<GoldenMineData> _goldenMineData;
		[field: SerializeField] public float BaseUnitsSpeed { get; private set; }

		public LeveledList<GoldenMineData> GoldenMines { get; private set; }

		private void OnEnable() => GoldenMines = new LeveledList<GoldenMineData>(_goldenMineData);
	}
}