using JetBrains.Annotations;

namespace Confrontation
{
	public class UnitFighter
	{
		private readonly UnitsSquad _squad;
		private readonly IAssetsService _assets;

		[CanBeNull] private Garrison _garrison;
		private IDefenceStrategy _defenceStrategy;
		private Cell _cell;

		public UnitFighter(UnitsSquad squad, IAssetsService assets)
		{
			_squad = squad;
			_assets = assets;
		}

		public void CaptureRegion(Cell cell)
		{
			_squad.Coordinates = cell.Coordinates;
			cell.RelatedRegion!.OwnerPlayerId = _squad.OwnerPlayerId;
		}

		public void FightWithSquadOn(Cell cell)
		{
			_cell = cell;

			_defenceStrategy = PickDefenceStrategy(_cell);
			var ourWholeStrength = _squad.AttackStrength;
			var theirWholeStrength = _defenceStrategy.DefenceStrength;
			var ourAdvantageRate = ourWholeStrength.CompareTo(theirWholeStrength);

			if (ourAdvantageRate > 0)
			{
				OurVictory();
			}
			else if (ourAdvantageRate < 0)
			{
				TheirVictory();
			}
			else
			{
				Draw();
			}
		}

		private IDefenceStrategy PickDefenceStrategy(Cell cell) => DefenceStrategyBase.Create(_assets, cell);

		private void OurVictory()
		{
			_squad.QuantityOfUnits -= _defenceStrategy.DefenceStrength;
			_defenceStrategy.Destroy();

			CaptureRegion(_cell);
		}

		private void TheirVictory()
		{
			_defenceStrategy.TakeDamage(_squad.AttackStrength);
			_assets.Destroy(_squad.gameObject);
		}

		private void Draw()
		{
			_defenceStrategy.Destroy();
			_assets.Destroy(_squad.gameObject);

			_cell.MakeRegionNeutral();
		}
	}
}