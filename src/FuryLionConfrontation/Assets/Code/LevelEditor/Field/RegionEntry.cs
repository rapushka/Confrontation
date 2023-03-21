using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class RegionEntry : MonoBehaviour, IInitializable
	{
		[Inject] private readonly int _id;
		[Inject] private readonly int _cellsCount;

		[SerializeField] private TextMeshProUGUI _regionIdTextMesh;
		[SerializeField] private string _regionIdPrefix;
		[Space]
		[SerializeField] private TextMeshProUGUI _cellsCountTextMesh;
		[SerializeField] private string _cellsCountPrefix;

		public int CellsCount
		{
			set => _cellsCountTextMesh.text = _cellsCountPrefix + value;
		}

		private int Id
		{
			set => _regionIdTextMesh.text = _regionIdPrefix + value;
		}

		public void Initialize()
		{
			Id = _id;
			CellsCount = _cellsCount;
		}

		private void OnValidate()
		{
			Id = 0;
			CellsCount = 0;
		}

		public class Factory : PlaceholderFactory<int, int, RegionEntry>
		{
			public override RegionEntry Create(int id, int cellsCount)
			{
				var regionEntry = base.Create(id, cellsCount);
				regionEntry.Initialize();
				return regionEntry;
			}
		}
	}
}