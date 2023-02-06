using Zenject;

namespace Confrontation
{
	public class Field : IField, IInitializable
	{
		[Inject] private ILevelSelector _levelSelector;

		public CoordinatedMatrix<Cell>     Cells     { get; private set; }
		public CoordinatedMatrix<Building> Buildings { get; private set; }

		public void Initialize()
		{
			Cells = new CoordinatedMatrix<Cell>(_levelSelector.SelectedLevel.Sizes);
			Buildings = new CoordinatedMatrix<Building>(_levelSelector.SelectedLevel.Sizes);
		}
	}
}