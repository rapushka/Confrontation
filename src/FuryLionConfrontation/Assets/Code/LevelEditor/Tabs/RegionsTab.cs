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

		[SerializeField] private Button _addRegionButton;
		[SerializeField] private Button _removeSelectedButton;
		[SerializeField] private Transform _regionsListRoot;

		private readonly List<RegionEntry> _regionEntries = new();

		private CellsToRegionsHandler _handler;
		[CanBeNull] private RegionEntry _selectedEntry;

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
			_addRegionButton.onClick.AddListener(AddRegion);
			_removeSelectedButton.onClick.AddListener(RemoveSelected);
		}

		private void OnDisable()
		{
			_addRegionButton.onClick.RemoveListener(AddRegion);
			_removeSelectedButton.onClick.RemoveListener(RemoveSelected);
		}

		private void OnDestroy()
		{
			foreach (var regionEntry in _regionEntries)
			{
				regionEntry.EntryClicked -= OnRegionEntryClicked;
			}
		}

		public override void Handle(Cell clickedCell) => _handler.Add(clickedCell);

		private void LoadRegions()
		{
			foreach (var fieldRegion in _field.Regions)
			{
				if (fieldRegion is null)
				{
					return;
				}

				var regionEntry = _regionEntries.SingleOrDefault((r) => r.Region.Id == fieldRegion.Id);
				if (regionEntry == false)
				{
					regionEntry = _regionEntryFactory.Create(_regionsListRoot, fieldRegion.Id);
					regionEntry.EntryClicked += OnRegionEntryClicked;
					_regionEntries.Add(regionEntry);
				}

				regionEntry!.Region = fieldRegion;
				_handler.Add(_field.Cells[fieldRegion.Coordinates], fieldRegion);
			}
		}

		private void AddRegion()
		{
			var regionEntry = _regionEntryFactory.Create(_regionsListRoot);
			regionEntry.EntryClicked += OnRegionEntryClicked;
			regionEntry.Region = _regionsFactory.Create();
			_regionEntries.Add(regionEntry);
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