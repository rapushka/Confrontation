using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildingsTab : LevelEditorPage
	{
		[Inject] private readonly LevelEditorBuildingButton.Factory _buildingButtonFactory;
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