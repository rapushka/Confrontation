using System;
using Confrontation.GameLogic;
using UnityEngine.UI;
using Zenject;

namespace Confrontation
{
	public class AccelerateTimeToggle : ToggleBase, IDisposable
	{
		[Inject] private readonly AccelerateableTimeServiceDecorator _timeService;

		public void Dispose() => _timeService.Decelerate();

		protected override void ToggleOn() => _timeService.Accelerate();

		protected override void ToggleOff() => _timeService.Decelerate();
	}
}