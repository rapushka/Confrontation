using Zenject;

namespace Confrontation
{
	public class NotEnoughKalymWindow : WindowBase
	{
		[Inject] private readonly ISoundService _playSound;

		public override void Open()
		{
			_playSound.UiError();
			base.Open();
		}
	}
}