using Zenject;

namespace Confrontation
{
	public class FieldGenerator : IInitializable
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly Cell.Factory _cellsFactory;

		public void Initialize() => GenerateField();

		private void GenerateField() => _field.Cells.DoubleFor(CreateHexagon);

		private void CreateHexagon(int i, int j)
		{
			var cell = _cellsFactory.Create();
			cell.Coordinates = new Coordinates(i, j);
		}
	}
}