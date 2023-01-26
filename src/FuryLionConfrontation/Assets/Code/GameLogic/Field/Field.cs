using Zenject;

namespace Confrontation
{
	public class Field
	{
		[Inject]
		public Field(ILevelSelector levelSelector)
			=> Cells = new CoordinatedMatrix<Cell>(levelSelector.SelectedLevel.Sizes);

		public CoordinatedMatrix<Cell> Cells { get; }
	}
}