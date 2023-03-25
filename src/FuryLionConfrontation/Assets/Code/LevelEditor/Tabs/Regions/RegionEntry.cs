using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class RegionEntry : SelectableEntryBase, IInitializable
	{
		[Inject] private readonly int _id;

		[Space]
		[SerializeField] private IntPrefixView _idView;
		[SerializeField] private IntPrefixView _cellsCountView;

		public Region Region { get; set; }

		public void Initialize()
		{
			_idView.Value = _id;
			_cellsCountView.Value = 0;
		}

		public void CalculateCellsCount() => _cellsCountView.Value = Region.CellsInRegion.Count();

		public class Factory : PlaceholderFactory<int, RegionEntry>
		{
			private int _currentRegionId;

			public RegionEntry Create() => Create(_currentRegionId++);

			public override RegionEntry Create(int id)
			{
				var regionEntry = base.Create(id);
				regionEntry.Initialize();
				return regionEntry;
			}
		}
	}
}