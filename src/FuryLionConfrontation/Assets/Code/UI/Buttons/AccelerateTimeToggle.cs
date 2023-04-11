using System;
using Confrontation.GameLogic;
using Zenject;

namespace Confrontation
{
	public class AccelerateTimeToggle : ToggleBase, IDisposable
	{
		[Inject] private readonly TimeServiceAccelerator _time;

		public void Dispose() => _time.Decelerate();

		protected override void ToggleOn() => _time.Accelerate();

		protected override void ToggleOff() => _time.Decelerate();
	}
}