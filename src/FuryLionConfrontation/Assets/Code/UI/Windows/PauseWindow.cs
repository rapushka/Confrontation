using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class PauseWindow : GameplayWindowBase
	{
		[Inject] private readonly TimeStopService _timeStopService;

		public override GameplayWindowBase Accept(IGameplayWindowVisitor visitor) => visitor.Visit(this);

		public override void Open()
		{
			_timeStopService.Stop();
			base.Open();
		}

		public override void Close()
		{
			base.Close();
			_timeStopService.Resume();
		}

		public new class Factory : PlaceholderFactory<Object, PauseWindow> { }
	}
}