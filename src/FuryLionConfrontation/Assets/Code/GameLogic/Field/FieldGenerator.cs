using Zenject;

namespace Confrontation
{
	public class FieldGenerator : IInitializable
	{
		[Inject] private readonly IResourcesService _resources;
		[Inject] private readonly IAssetsService _assets;
		[Inject] private readonly IField _field;

		public void Initialize() => GenerateField();

		private void GenerateField() => _field.Cells.SetForEach(CreateHexagon);

		private Cell CreateHexagon(int i, int j)
			=> _assets.Instantiate(original: _resources.CellPrefab, InstantiateGroup.Cells)
			          .With((c) => c.Coordinates = new Coordinates(i, j));
	}
}