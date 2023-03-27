using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class RegionOwnershipEntry : SelectableEntryBase
	{
		[Inject] private readonly Region _region;

		[SerializeField] private IntPrefixView _idView;
		[SerializeField] private TMP_InputField _ownerIdInputField;

		public int OwnerId
		{
			get => int.Parse(_ownerIdInputField.text);
			private set => _ownerIdInputField.text = value.ToString();
		}

		public int    Id     => _idView.Value;
		public Region Region => _region;

		private void Initialize()
		{
			_idView.Value = _region.Id;
			OwnerId = _region.OwnerPlayerId;
		}

		public class Factory : PlaceholderFactory<Region, RegionOwnershipEntry>
		{
			public override RegionOwnershipEntry Create(Region region)
			{
				var entry = base.Create(region);
				entry.Initialize();
				return entry;
			}
		}
	}
}