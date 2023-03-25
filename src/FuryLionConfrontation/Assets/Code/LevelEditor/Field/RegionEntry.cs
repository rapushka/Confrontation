using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class RegionEntry : EntryBase, IInitializable
	{
		[Inject] private readonly int _id;

		[Space]
		[SerializeField] private TextMeshProUGUI _regionIdTextMesh;
		[SerializeField] private string _regionIdPrefix;
		[Space]
		[SerializeField] private TextMeshProUGUI _cellsCountTextMesh;
		[SerializeField] private string _cellsCountPrefix;

		public Region Region { get; set; }

		private int CellsCount { set => _cellsCountTextMesh.text = _cellsCountPrefix + value; }

		private int Id { set => _regionIdTextMesh.text = _regionIdPrefix + value; }

		public void Initialize()
		{
			Id = _id;
			CellsCount = 0;
		}

		private void OnValidate()
		{
			Id = 0;
			CellsCount = 0;
		}

		public void CalculateCellsCount() => CellsCount = Region.CellsInRegion.Count();

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