using Zenject;

namespace Confrontation
{
	public interface IField
	{
		CoordinatedMatrix<Cell> Cells { get; }
	}

	public class Field : IField
	{
		[Inject]
		public Field(ILevelSelector levelSelector)
			=> Cells = new CoordinatedMatrix<Cell>(levelSelector.SelectedLevel.Sizes);

		public CoordinatedMatrix<Cell> Cells { get; }
	}
}