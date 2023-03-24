using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Confrontation
{
	public class PlayersOwnershipTab : LevelEditorPage
	{
		[Inject] private readonly RegionOwnershipEntry.Factory _regionOwnershipEntryFactory;
		[Inject] private readonly IField _field;

		[SerializeField] private Transform _regionsListRoot;
		[SerializeField] private Button _applyButton;

		private readonly List<RegionOwnershipEntry> _entries = new();

		private void OnEnable()
		{
			_applyButton.onClick.AddListener(UpdateAllOwners);

			foreach (var region in _field.Regions.WithoutNulls().OnlyUnique())
			{
				var regionOwnershipEntry = _regionOwnershipEntryFactory.Create(region.Id, _regionsListRoot);
				regionOwnershipEntry.OwnerId = region.OwnerPlayerId;
				_entries.Add(regionOwnershipEntry);
			}
		}

		private void OnDisable()
		{
			_applyButton.onClick.RemoveListener(UpdateAllOwners);

			_entries.Clear();
		}

		private void UpdateAllOwners()
		{
			foreach (var regionOwnershipEntry in _entries)
			{
				var region = _field.Regions.First((r) => r.Id == regionOwnershipEntry.Id);
				region.OwnerPlayerId = regionOwnershipEntry.OwnerId;
			}
		}

		public override void Handle(Cell clickedCell) { }
	}
}