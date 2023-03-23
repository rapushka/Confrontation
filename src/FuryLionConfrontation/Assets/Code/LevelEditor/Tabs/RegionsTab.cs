using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Confrontation
{
	public class RegionsTab : LevelEditorPage
	{
		[Inject] private readonly RegionEntry.Factory _regionEntryFactory;
		[Inject] private readonly IField _field;
		[Inject] private readonly Region.Factory _regionsFactory;
		[Inject] private readonly ILevelSelector _levelSelector;

		[SerializeField] private Button _addRegionButton;
		[SerializeField] private Button _removeSelectedButton;
		[SerializeField] private Transform _regionsListRoot;

		private readonly List<RegionEntry> _regionEntries = new();

		private CellsToRegionsHandler _handler;
		[CanBeNull] private RegionEntry _selectedEntry;

		private IEnumerable<Region.Data> RegionsData => _levelSelector.SelectedLevel.Regions;

		public RegionEntry SelectedEntry
		{
			get => _selectedEntry;
			private set
			{
				if (_selectedEntry is not null)
				{
					_selectedEntry!.Deselect();
				}

				_selectedEntry = value;
				_selectedEntry!.Select();
			}
		}

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

		private void OnDestroy() => _regionEntries.ForEach((r) => r.EntryClicked -= OnRegionEntryClicked);

		public override void Handle(Cell clickedCell)
		{
			_handler.Add(clickedCell);
			_regionEntries.ForEach(CalculateCellCount);
		}

		private void LoadRegions() => RegionsData.Select(AsRegion).ForEach(CreateRegionEntry);

		private Region AsRegion(Region.Data data) => _field.Regions[data.CellsCoordinates.First()];

		private void CreateRegionEntry() => ToRegionEntry(_regionsFactory.Create());

		private void CreateRegionEntry(Region region)
		{
			var regionEntry = ToRegionEntry(region);
			CalculateCellCount(regionEntry);
		}

		private static void CalculateCellCount(RegionEntry regionEntry)
			=> regionEntry.CellsCount = regionEntry.Region.CellsInRegion.Count();

		private RegionEntry ToRegionEntry(Region region)
		{
			var regionEntry = _regionEntryFactory.Create(_regionsListRoot);
			regionEntry.EntryClicked += OnRegionEntryClicked;
			regionEntry.Region = region;
			_regionEntries.Add(regionEntry);
			return regionEntry;
		}

		private void RemoveSelected()
		{
			if (_selectedEntry != false)
			{
				_regionEntries.Remove(_selectedEntry);
				Destroy(_selectedEntry!.gameObject);
				_selectedEntry = null;
			}
		}

		private void OnRegionEntryClicked(RegionEntry clicked) => SelectedEntry = clicked;
	}
}