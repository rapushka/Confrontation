using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class TutorialWindow : GameplayWindowBase
	{
		[Inject] private readonly TimeStopService _timeStopService;

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

		public override GameplayWindowBase Accept(IGameplayWindowVisitor visitor) => visitor.Visit(this);

		public new class Factory : PlaceholderFactory<Object, TutorialWindow> { }
	}
}