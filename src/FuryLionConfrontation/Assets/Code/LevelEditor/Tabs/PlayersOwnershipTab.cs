using System;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class PlayersOwnershipTab : LevelEditorPage
	{
		[Inject] private readonly RegionOwnershipEntry.Factory _regionOwnershipEntryFactory;
		[Inject] private readonly RegionsTab _regionsTab;

		[SerializeField] private Transform _regionsListRoot;

		private void OnEnable()
		{
			foreach (var regionEntry in _regionsTab.RegionEntries)
			{
				var regionOwnershipEntry = _regionOwnershipEntryFactory.Create(regionEntry.Id, _regionsListRoot);
			}
		}

		public override void Handle(Cell clickedCell) { }
	}
}