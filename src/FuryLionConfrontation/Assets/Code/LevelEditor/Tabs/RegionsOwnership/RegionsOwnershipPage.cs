using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Confrontation
{
	public class RegionsOwnershipPage : SelectableListPage<RegionOwnershipEntry>, IRegionSelector
	{
		[Inject] private readonly RegionOwnershipEntry.Factory _regionOwnershipEntryFactory;
		[Inject] private readonly IField _field;

		[SerializeField] private Button _applyButton;

		public Region SelectedRegion => SelectedEntry.Region;

		private void OnEnable()
		{
			_applyButton.onClick.AddListener(UpdateAllOwners);

			LoadRegions();
		}

		private void OnDisable()
		{
			_applyButton.onClick.RemoveListener(UpdateAllOwners);

			ForEachEntry((roe) => Destroy(roe.gameObject));
			ClearList();
		}

		public override void Handle(Cell clickedCell) { }

		public void UpdateAllOwners() => ForEachEntry(UpdateOwner);

		private void LoadRegions()
			=> _field.Regions
			         .WithoutNulls()
			         .OnlyUnique()
			         .Select(_regionOwnershipEntryFactory.Create)
			         .ForEach(AddEntry);

		private void UpdateOwner(RegionOwnershipEntry regionOwnershipEntry)
		{
			var region = _field.Regions.WithoutNulls().First((r) => r.Id == regionOwnershipEntry.Id);
			region.OwnerPlayerId = regionOwnershipEntry.OwnerId;
		}
	}
}