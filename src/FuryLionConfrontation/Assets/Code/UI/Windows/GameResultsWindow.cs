using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public class GameResultsWindow : GameplayWindowBase
	{
		[Inject] private readonly User _user;

		[SerializeField] private GameObject _victoryTitle;
		[SerializeField] private GameObject _looseTitle;

		private void Start()
		{
			_victoryTitle.SetActive(_user.GameResult is GameResult.Victory);
			_looseTitle.SetActive(_user.GameResult is GameResult.Loose);
		}

		public override GameplayWindowBase Accept(IGameplayWindowVisitor visitor) => visitor.Visit(this);

		public new class Factory : PlaceholderFactory<Object, GameResultsWindow> { }
	}
}