using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class RegionOwnershipEntry : MonoBehaviour
	{
		[Inject] private readonly int _id;

		[SerializeField] private IntPrefixView _idView;
		[SerializeField] private TMP_InputField _ownerIdInputField;

		public int OwnerId
		{
			get => int.Parse(_ownerIdInputField.text);
			private set => _ownerIdInputField.text = value.ToString();
		}

		public int Id => _idView.Value;

		private void Initialize()
		{
			_idView.Value = _id;
			OwnerId = 0;
		}

		public class Factory : PlaceholderFactory<int, RegionOwnershipEntry>
		{
			public RegionOwnershipEntry Create(Region region, Transform parent)
			{
				var entry = Create(region.Id);
				entry.Initialize();
				entry.transform.SetParent(parent);
				entry.OwnerId = region.OwnerPlayerId;
				return entry;
			}
		}
	}
}