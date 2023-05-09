using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public class GameResultsWindow : GameplayWindowBase
	{
		[Inject] private readonly User _user;
		[Inject] private readonly ISoundService _playSound;

		[SerializeField] private GameObject _victoryTitle;
		[SerializeField] private GameObject _looseTitle;

		private void Start()
		{
			var isVictory = _user.GameResult is GameResult.Victory;

			PlaySound(isVictory);

			_victoryTitle.SetActive(isVictory);
			_looseTitle.SetActive(isVictory == false);
		}

		private void PlaySound(bool isVictory)
		{
			if (isVictory)
			{
				_playSound.Victory();
			}
			else
			{
				_playSound.Loose();
			}
		}

		public override GameplayWindowBase Accept(IGameplayWindowVisitor visitor) => visitor.Visit(this);

		public new class Factory : PlaceholderFactory<Object, GameResultsWindow> { }
	}
}