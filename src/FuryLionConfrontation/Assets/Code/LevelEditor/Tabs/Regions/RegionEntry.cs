using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class RegionEntry : SelectableEntryBase, IInitializable
	{
		[Inject] private readonly Region _region;

		[Space]
		[SerializeField] private IntPrefixView _idView;
		[SerializeField] private IntPrefixView _cellsCountView;

		public Region Region => _region;

		public void Initialize()
		{
			_idView.Value = _region.Id;
			_cellsCountView.Value = 0;
		}

		public void CalculateCellsCount() => _cellsCountView.Value = Region.CellsInRegion.Count();

		public class Factory : PlaceholderFactory<Region, RegionEntry>
		{
			public override RegionEntry Create(Region region)
			{
				var regionEntry = base.Create(region);
				regionEntry.Initialize();
				return regionEntry;
			}
		}
	}
}