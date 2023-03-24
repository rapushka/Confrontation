using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class RegionOwnershipEntry : MonoBehaviour
	{
		[Inject] private readonly int _id;

		[SerializeField] private TextMeshProUGUI _regionIdTextMesh;
		[SerializeField] private string _regionIdPrefix;
		[Space]
		[SerializeField] private TMP_InputField _ownerIdInputField;

		public int OwnerId => int.Parse(_ownerIdInputField.text);

		public TMP_InputField OwnerIdInputField => _ownerIdInputField;

		public int Id { get => _id; private set => _regionIdTextMesh.text = _regionIdPrefix + value; }

		private void Initialize()
		{
			Id = _id;
			_ownerIdInputField.text = 0.ToString();
		}

		public class Factory : PlaceholderFactory<int, RegionOwnershipEntry>
		{
			public RegionOwnershipEntry Create(int id, Transform parent)
			{
				var entry = Create(id);
				entry.transform.SetParent(parent);
				return entry;
			}

			public override RegionOwnershipEntry Create(int id)
			{
				var entry = base.Create(id);
				entry.Initialize();
				return entry;
			}
		}
	}
}