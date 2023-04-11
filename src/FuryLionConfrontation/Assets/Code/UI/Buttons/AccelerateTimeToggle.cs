using System;
using Confrontation.GameLogic;
using Zenject;

namespace Confrontation
{
	public class AccelerateTimeToggle : ToggleBase, IDisposable
	{
		[Inject] private readonly AccelerableTimeServiceDecorator _timeService;

		public void Dispose() => _timeService.Decelerate();

		protected override void ToggleOn() => _timeService.Accelerate();

		protected override void ToggleOff() => _timeService.Decelerate();
	}
}