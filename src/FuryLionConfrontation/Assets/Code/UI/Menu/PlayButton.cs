using Zenject;

namespace Confrontation
{
	public class PlayButton : ButtonBase
	{
		[Inject] private readonly ToGameplay _toGameplay;

		protected override void OnButtonClick() => _toGameplay.Transfer();
	}
}