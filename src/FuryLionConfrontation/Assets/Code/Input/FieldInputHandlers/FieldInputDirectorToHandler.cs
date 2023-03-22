namespace Confrontation
{
	public class FieldInputDirectorToHandler : FieldInputDirectorBase
	{
		private readonly IFieldClickHandler _handler;

		public FieldInputDirectorToHandler(IFieldClickHandler handler) => _handler = handler;

		protected override void OnCellClick(Cell cell) => _handler.Handle(cell);
	}
}