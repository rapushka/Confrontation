using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class EnemyBuildingBuilder
	{
		[Inject] private readonly Player _player;
		[Inject] private readonly IField _field;
		[Inject] private readonly Purchase _purchase;

		private IEnumerable<Cell> OurEmptyCells 
			=> _field.Cells.Where((c) => c.OwnerPlayerId == _player.Id).Where((c) => c.IsEmpty);

		public void Build()
		{
			
			var randomEmptyCell = OurEmptyCells.PickRandom();
		}

		public class Factory : PlaceholderFactory<Player, EnemyBuildingBuilder> { }
	}
}