using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Confrontation
{
	public class RegionEntry : ButtonBase, IInitializable
	{
		[Inject] private readonly int _id;

		[SerializeField] private TextMeshProUGUI _regionIdTextMesh;
		[SerializeField] private string _regionIdPrefix;
		[Space]
		[SerializeField] private TextMeshProUGUI _cellsCountTextMesh;
		[SerializeField] private string _cellsCountPrefix;
		[Space]
		[SerializeField] private Image _selectionImage;

		public event Action<RegionEntry> EntryClicked;
		
		public Region Region { get; set; }

		public int CellsCount { set => _cellsCountTextMesh.text = _cellsCountPrefix + value; }

		private int Id { set => _regionIdTextMesh.text = _regionIdPrefix + value; }

		private bool Selected { set => _selectionImage.enabled = value; }

		protected override void OnButtonClick()
		{
			EntryClicked?.Invoke(this);
			Selected = true;
		}

		public void Initialize()
		{
			Id = _id;
			CellsCount = 0;
			Selected = false;
		}

		public void Select() => Selected = true;

		public void Deselect() => Selected = false;

		private void OnValidate()
		{
			Id = 0;
			CellsCount = 0;
		}

		public class Factory : PlaceholderFactory<int, RegionEntry>
		{
			private int _currentRegionId;

			public RegionEntry Create(Transform parent)
			{
				var regionEntry = Create();
				regionEntry.transform.SetParent(parent);
				return regionEntry;
			}

			public RegionEntry Create()
			{
				var regionEntry = base.Create(_currentRegionId++);
				regionEntry.Initialize();
				return regionEntry;
			}
		}
	}
}