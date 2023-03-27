using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Confrontation
{
	public class RegionsPage : SelectableListPage<RegionEntry>
	{
		[Inject] private readonly RegionEntry.Factory _regionEntryFactory;
		[Inject] private readonly IField _field;
		[Inject] private readonly Region.Factory _regionsFactory;
		[Inject] private readonly ILevelSelector _levelSelector;

		[SerializeField] private Button _addRegionButton;
		[SerializeField] private Button _removeSelectedButton;

		private CellsToRegionsHandler _handler;

		private IEnumerable<Region.Data> RegionsData => _levelSelector.SelectedLevel.Regions;

		private void Start()
		{
			_handler = new CellsToRegionsHandler(this);
			LoadRegions();
		}

		private void OnEnable()
		{
			_addRegionButton.onClick.AddListener(CreateRegionEntry);
			_removeSelectedButton.onClick.AddListener(RemoveSelected);
		}

		private void OnDisable()
		{
			_addRegionButton.onClick.RemoveListener(CreateRegionEntry);
			_removeSelectedButton.onClick.RemoveListener(RemoveSelected);
		}

		public override void Handle(Cell clickedCell)
		{
			_handler.ToggleCellMembershipInSelectedRegion(clickedCell);
			ForEachEntry((re) => re.CalculateCellsCount());
		}

		private void LoadRegions() => RegionsData.Select(AsRegion).ForEach(CreateRegionEntry);

		private Region AsRegion(Region.Data data) => _field.Regions[data.CellsCoordinates.First()];

		private void CreateRegionEntry() => CreateRegionEntry(_regionsFactory.Create());

		private void CreateRegionEntry(Region region)
		{
			var entry = _regionEntryFactory.Create(region);
			AddEntry(entry);
			entry.CalculateCellsCount();
		}
	}
}