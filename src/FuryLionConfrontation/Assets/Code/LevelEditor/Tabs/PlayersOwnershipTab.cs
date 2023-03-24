using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class PlayersOwnershipTab : LevelEditorPage
	{
		[Inject] private readonly RegionOwnershipEntry.Factory _regionOwnershipEntryFactory;
		[Inject] private readonly RegionsTab _regionsTab;

		[SerializeField] private Transform _regionsListRoot;

		private readonly List<RegionOwnershipEntry> _entries = new();

		private void OnEnable()
		{
			foreach (var regionEntry in _regionsTab.RegionEntries)
			{
				var regionOwnershipEntry = _regionOwnershipEntryFactory.Create(regionEntry.Id, _regionsListRoot);
				regionOwnershipEntry.OwnerIdInputField.onEndEdit.AddListener(OnOwnerChanged);
				_entries.Add(regionOwnershipEntry);
			}
		}

		private void OnDisable()
			=> _entries.ForEach((r) => r.OwnerIdInputField.onEndEdit.RemoveListener(OnOwnerChanged));

		private void OnOwnerChanged(string text) => UpdateAllOwners();

		private void UpdateAllOwners()
		{
			foreach (var regionOwnershipEntry in _entries)
			{
				var region = _regionsTab.RegionEntries.Single((r) => r.Id == regionOwnershipEntry.Id).Region;
				region.OwnerPlayerId = regionOwnershipEntry.OwnerId;
			}
		}

		public override void Handle(Cell clickedCell) { }
	}
}