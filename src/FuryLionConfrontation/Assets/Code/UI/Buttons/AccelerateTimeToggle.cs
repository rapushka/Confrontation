using System;
using Confrontation.GameLogic;
using Zenject;

namespace Confrontation
{
	public class AccelerateTimeToggle : ToggleBase, IDisposable
	{
		[Inject] private readonly TimeAccelerationService _timeAcceleration;

		public void Dispose() => _timeAcceleration.Decelerate();

		protected override void ToggleOn() => _timeAcceleration.Accelerate();

		protected override void ToggleOff() => _timeAcceleration.Decelerate();
	}
}