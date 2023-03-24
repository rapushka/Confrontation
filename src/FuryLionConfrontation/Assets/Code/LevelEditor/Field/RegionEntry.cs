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
		[Space]
		[SerializeField] private string _cellsCountPrefix;
		[SerializeField] private Image _selectionImage;

		private int _cellsCount;

		public event Action<RegionEntry> EntryClicked;

		public Region Region { get; set; }

		public int CellsCount
		{
			get => _cellsCount;
			set
			{
				_cellsCount = value;
				_cellsCountTextMesh.text = _cellsCountPrefix + _cellsCount;
			}
		}

		public int Id { get => _id; private set => _regionIdTextMesh.text = _regionIdPrefix + value; }

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
				var regionEntry = Create(_currentRegionId++);
				regionEntry.transform.SetParent(parent);
				return regionEntry;
			}

			public override RegionEntry Create(int id)
			{
				var regionEntry = base.Create(id);
				regionEntry.Initialize();
				return regionEntry;
			}
		}
	}
}