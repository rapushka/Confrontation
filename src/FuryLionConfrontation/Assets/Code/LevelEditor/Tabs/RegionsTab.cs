using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Confrontation
{
	public class RegionsTab : MonoBehaviour
	{
		[Inject] private readonly RegionEntry.Factory _regionEntryFactory;

		[SerializeField] private Button _addRegionButton;
		[SerializeField] private Button _removeSelectedButton;
		[SerializeField] private Transform _regionsListRoot;

		private readonly List<RegionEntry> _regions = new();

		[CanBeNull] private RegionEntry _selectedEntry;

		[NotNull]
		private RegionEntry SelectedEntry
		{
			set
			{
				if (_selectedEntry is not null)
				{
					_selectedEntry.Selected = false;
				}

				_selectedEntry = value;
				_selectedEntry.Selected = true;
			}
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
			foreach (var regionEntry in _regions)
			{
				regionEntry.EntryClicked -= OnRegionEntryClicked;
			}
		}

		private void AddRegion()
		{
			var regionEntry = _regionEntryFactory.Create(_regionsListRoot);
			regionEntry.EntryClicked += OnRegionEntryClicked;
			_regions.Add(regionEntry);
		}

		private void RemoveSelected()
		{
			if (_selectedEntry is not null)
			{
				_regions.Remove(_selectedEntry);
			}
		}

		private void OnRegionEntryClicked(RegionEntry clicked) => SelectedEntry = clicked;
	}
}