using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildingsPage : LevelEditorPage
	{
		[Inject] private readonly BuildingEntry.Factory _buildingButtonFactory;
		[Inject] private readonly IResourcesService _resources;

		[SerializeField] private Transform _buildingButtonsRoot;

		private void Start()
		{
			foreach (var building in _resources.Buildings)
			{
				_buildingButtonFactory.Create(building, _buildingButtonsRoot);
			}
		}

		public override void Handle(Cell clickedCell) { }
	}
}