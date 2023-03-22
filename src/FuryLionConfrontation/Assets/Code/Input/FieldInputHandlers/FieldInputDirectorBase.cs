using System;
using Zenject;

namespace Confrontation
{
	public abstract class FieldInputDirectorBase : IInitializable, IDisposable
	{
		[Inject] protected readonly IInputService InputService;
		[Inject] protected readonly User User;

		public virtual void Initialize() => InputService.Clicked += OnClick;

		public virtual void Dispose() => InputService.Clicked -= OnClick;

		private void OnClick(ClickReceiver clickReceiver)
		{
			InputService.ClickedCell = clickReceiver.Cell;
			OnCellClick(clickReceiver.Cell);
		}

		protected abstract void OnCellClick(Cell cell);
	}
}