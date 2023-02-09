using System.Linq;
using Zenject;

namespace Confrontation
{
	public class PlayersGenerator : IInitializable
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly GameplayLoop _gameplayLoop;

		public void Initialize() => Bind();

		private void Bind()
		{
			foreach (var capital in _field.Buildings.OfType<Capital>())
			{
				var player = new Player
				{
					Id = capital.RelatedCell.RelatedRegion!.OwnerPlayerId,
					Capital = capital,
				};
				_gameplayLoop.Players.Add(player);
			}
		}
	}
}