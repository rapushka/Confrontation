using Zenject;

namespace Confrontation
{
	public class Field : IInitializable
	{
		[Inject] private readonly IResourcesService _resources;
		[Inject] private readonly IAssetsService _assets;
		[Inject] private readonly User _user;

		public CoordinatedMatrix<Cell> Cells { get; private set; }

		public void Initialize()
		{
			Cells = new CoordinatedMatrix<Cell>(_user.SelectedLevel.Sizes);
			GenerateField();
		}

		private void GenerateField() => Cells.SetForEach(CreateHexagon);

		private Cell CreateHexagon(int i, int j)
			=> _assets.Instantiate(original: _resources.CellPrefab, InstantiateGroup.Cells)
			          .With((c) => c.Coordinates = new Coordinates(i, j));
	}
}