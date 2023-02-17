using JetBrains.Annotations;

namespace Confrontation
{
	public class UnitFighter
	{
		private readonly UnitsSquad _squad;
		private readonly IAssetsService _assets;
		private readonly IField _field;

		private int _garrisonQuantity;
		private int _theyQuantity;
		private Garrison _they;
		private Cell _cell;
		[CanBeNull] private Garrison _garrison;
		private IDefenceStrategy _defenceStrategy;

		public UnitFighter(UnitsSquad squad, IAssetsService assets, IField field)
		{
			_squad = squad;
			_assets = assets;
			_field = field;
		}

		private int ComposedDefenceForcesQuantity => _theyQuantity + _garrisonQuantity;

		public void CaptureRegion(Cell cell)
		{
			_squad.Coordinates = cell.Coordinates;
			cell.RelatedRegion!.OwnerPlayerId = _squad.OwnerPlayerId;
		}

		public void FightWithSquadOn(Cell cell)
		{
			_cell = cell;

			_they = _cell.LocatedUnits;
			_garrison = _field.Garrisons[_cell.Coordinates];

			_defenceStrategy = PickDefenceStrategy(_they, _garrison);

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

		private IDefenceStrategy PickDefenceStrategy(Garrison they, Garrison garrison)
		{
			throw new System.NotImplementedException();
		}

		private void OurVictory()
		{
			_squad.QuantityOfUnits -= ComposedDefenceForcesQuantity;

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