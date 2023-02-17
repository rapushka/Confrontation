using JetBrains.Annotations;

namespace Confrontation
{
	public class UnitFighter : ICanLoseDefenders
	{
		private readonly UnitsSquad _squad;
		private readonly IAssetsService _assets;
		private readonly IField _field;

		[CanBeNull] private Garrison _garrison;
		private IDefenceStrategy _defenceStrategy;
		private Cell _cell;

		public UnitFighter(UnitsSquad squad, IAssetsService assets, IField field)
		{
			_squad = squad;
			_assets = assets;
			_field = field;
		}

		public void CaptureRegion(Cell cell)
		{
			_squad.Coordinates = cell.Coordinates;
			cell.RelatedRegion!.OwnerPlayerId = _squad.OwnerPlayerId;
		}

		public void FightWithSquadOn(Cell cell)
		{
			_cell = cell;

			_defenceStrategy = PickDefenceStrategy(_cell.LocatedUnits, _cell.Garrison);
			var fightResult = _squad.QuantityOfUnits.CompareTo(_defenceStrategy.Quantity);

			if (fightResult < 0)
			{
				OurVictory();
			}
			else if (fightResult > 0)
			{
				TheirVictory();
			}
			else
			{
				Draw();
			}
		}

		void ICanLoseDefenders.LoseDefenders() => _cell.MakeRegionNeutral();

		private IDefenceStrategy PickDefenceStrategy(Garrison they, Garrison garrison)
			=> DefenceStrategyBase.Create(_assets, this, they, garrison);

		private void OurVictory()
		{
			_squad.QuantityOfUnits -= _defenceStrategy.Quantity;

			_defenceStrategy.Destroy();

			CaptureRegion(_cell);
		}

		private void TheirVictory()
		{
			_defenceStrategy.TakeDamage(_squad.QuantityOfUnits);

			_assets.Destroy(_squad.gameObject);
		}

		private void Draw()
		{
			_defenceStrategy.Destroy();

			_field.LocatedUnits.Remove(_squad);
			_assets.Destroy(_squad.gameObject);

			_cell.MakeRegionNeutral();
		}
	}
}