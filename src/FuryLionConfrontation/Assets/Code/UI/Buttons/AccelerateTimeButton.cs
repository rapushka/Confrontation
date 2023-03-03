using System;
using Confrontation.GameLogic;
using Zenject;

namespace Confrontation
{
	public class AccelerateTimeButton : ToggleButtonBase, IDisposable
	{
		[Inject] private readonly AccelerateableTimeServiceDecorator _timeService;

		public void Dispose() => _timeService.Decelerate();

		protected override void OnToggleClicked() => _timeService.Accelerate();

		protected override void OnToggleUnClicked() => _timeService.Decelerate();
	}
}