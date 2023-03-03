using Confrontation.GameLogic;
using Zenject;

namespace Confrontation
{
	public class AccelerateTimeButton : ToggleButtonBase
	{
		[Inject] private readonly AccelerateableTimeServiceDecorator _timeService;

		private void OnDestroy() => _timeService.Decelerate();

		protected override void OnToggleClicked() => _timeService.Accelerate();

		protected override void OnToggleUnClicked() => _timeService.Decelerate();
	}
}