using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Confrontation
{
	public class RegionsTab : MonoBehaviour, IFieldClickHandler
	{
		[Inject] private readonly RegionEntry.Factory _regionEntryFactory;
		[Inject] private readonly IField _field;
		[Inject] private readonly Region.Factory _regionsFactory;

		[SerializeField] private Button _addRegionButton;
		[SerializeField] private Button _removeSelectedButton;
		[SerializeField] private Transform _regionsListRoot;

		private readonly List<RegionEntry> _regions = new();

		private RegionsStateClickHandler _handler;
		[CanBeNull] private RegionEntry _selectedEntry;

		[NotNull]
		public RegionEntry SelectedEntry
		{
			get => _selectedEntry ? _selectedEntry : throw new InvalidOperationException();
			private set
			{
				if (_selectedEntry is not null)
				{
					_selectedEntry!.Deselect();
				}

				_selectedEntry = value;
				_selectedEntry.Select();
			}
		}

		public IField Field => _field;

		private void Start() => _handler = new RegionsStateClickHandler(this);

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
			foreach (var regionEntry in _regions)
			{
				regionEntry.EntryClicked -= OnRegionEntryClicked;
			}
		}

		public void Handle(Cell clickedCell) => _handler.Handle(clickedCell);

		private void AddRegion()
		{
			var regionEntry = _regionEntryFactory.Create(_regionsListRoot);
			regionEntry.EntryClicked += OnRegionEntryClicked;
			regionEntry.Region = _regionsFactory.Create();
			_regions.Add(regionEntry);
		}

		private void RemoveSelected()
		{
			if (_selectedEntry != false)
			{
				_regions.Remove(_selectedEntry);
				Destroy(_selectedEntry!.gameObject);
				_selectedEntry = null;
			}
		}

		private void OnRegionEntryClicked(RegionEntry clicked) => SelectedEntry = clicked;
	}
}