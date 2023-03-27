using Zenject;

namespace Confrontation
{
	public class BuildingsPage : SelectableListPage<BuildingEntry>
	{
		[Inject] private readonly BuildingEntry.Factory _buildingEntryFactory;
		[Inject] private readonly BuildingSpawner _buildingSpawner;

		private void Start()
		{
			foreach (var buildingName in BuildingsCollection.BuildingsNames)
			{
				var building = BuildingsCollection.Load(buildingName);
				AddEntry(_buildingEntryFactory.Create(building));
			}
		}

		public override void Handle(Cell clickedCell)
		{
			if (clickedCell.Building == true)
			{
				Destroy(clickedCell.Building!.gameObject);
			}

			_buildingSpawner.Build(SelectedEntry.Building, clickedCell);
		}
	}
}