using Confrontation.GameLogic;
using Zenject;

namespace Confrontation
{
	public class AccelerateTimeButton : ButtonBase
	{
		[Inject] private readonly AccelerateableTimeServiceDecorator _timeService;

		private bool _clicked;

		protected override void OnButtonClick()
		{
			if (_clicked)
			{
				_clicked = false;

				_timeService.Decelerate();
			}
			else
			{
				_clicked = true;

				_timeService.Accelerate();
			}
		}

		private void OnDestroy() => _timeService.Decelerate();
	}
}