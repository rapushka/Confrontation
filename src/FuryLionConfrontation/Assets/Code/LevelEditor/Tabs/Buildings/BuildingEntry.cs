using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildingEntry : SelectableEntryBase
	{
		[Inject] private readonly Building _building;

		[SerializeField] private TextMeshProUGUI _buildingNameTextMesh;

		public Building Building => _building;

		private string TextView => _building.Name;

		private void Start() => _buildingNameTextMesh.text = TextView;

		public class Factory : PlaceholderFactory<Building, BuildingEntry> { }
	}
}