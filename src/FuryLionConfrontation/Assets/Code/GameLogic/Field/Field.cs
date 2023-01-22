using Zenject;

namespace Confrontation
{
	public class Field : IInitializable
	{
		private readonly IResourcesService _resources;
		private readonly IAssetsService _assets;

		[Inject]
		public Field(IResourcesService resources, IAssetsService assets)
		{
			_resources = resources;
			_assets = assets;

			Cells = new CoordinatedMatrix<Cell>(_resources.CurrentLevel.Sizes);
		}

		public CoordinatedMatrix<Cell> Cells { get; }

		public void Initialize() => GenerateField();

		public void GenerateField() => Cells.SetForEach(CreateHexagon);

		private Cell CreateHexagon(int i, int j)
			=> _assets.Instantiate(original: _resources.CellPrefab, InstantiateGroup.Cells)
			          .With((c) => c.Coordinates = new Coordinates(i, j));
	}
}