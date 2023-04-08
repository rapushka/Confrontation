using JetBrains.Annotations;

namespace Confrontation
{
	public class UnitFighter
	{
		private readonly IAssetsService _assets;

		[CanBeNull] private Garrison _garrison;
		private Cell _cell;
		private (float Attackers, float Defenders) _cachedSidesHealth;

		public UnitFighter(UnitsSquad squad, IAssetsService assets)
		{
			Attackers = squad;
			_assets = assets;
		}

		private UnitsSquad Attackers { get; }

		private IDefenceStrategy Defenders { get; set; }

		private bool IsDefendersAlive => Defenders.QuantityOfUnits > 0;

		private bool IsAttackersAlive => Attackers.QuantityOfUnits > 0;

		public void CaptureRegion(Cell cell)
		{
			Attackers.Coordinates = cell.Coordinates;
			cell.RelatedRegion!.OwnerPlayerId = Attackers.OwnerPlayerId;
		}

		public void FightWithSquadOn(Cell cell)
		{
			_cell = cell;
			Defenders = PickDefenceStrategy(_cell);

			FightToDeath();
			DetermineWinner();
		}

		private void FightToDeath()
		{
			while (IsAttackersAlive && IsDefendersAlive)
			{
				if (Attackers.HealthPoints.IsEqualFloats(_cachedSidesHealth.Attackers)
				    && Defenders.HealthPoints.IsEqualFloats(_cachedSidesHealth.Defenders))
				{
					KillBoth();
					break;
				}

				_cachedSidesHealth = (Attackers.HealthPoints, Defenders.HealthPoints);

				var defendersDamage = Defenders.BaseDamage;
				var attackersDamage = Attackers.AttackDamage;

				Attackers.Health.TakeDamage(defendersDamage);
				Defenders.TakeDamageOnDefence(attackersDamage, Attackers.DefencePierceRate);
			}
		}

		private void KillBoth()
		{
			Defenders.Kill();
			Attackers.Kill();
		}

		private void DetermineWinner()
		{
			if (IsAttackersAlive)
			{
				AttackersWin();
				return;
			}

			if (IsDefendersAlive)
			{
				DefendersWin();
				return;
			}

			Draw();
		}

		private IDefenceStrategy PickDefenceStrategy(Cell cell) => DefenceStrategyBase.Create(_assets, cell);

		private void AttackersWin()
		{
			Defenders.Destroy();

			CaptureRegion(_cell);
		}

		private void DefendersWin() => DestroyAttackers();

		private void Draw()
		{
			Defenders.Destroy();
			DestroyAttackers();

			_cell.MakeRegionNeutral();
		}

		private void DestroyAttackers() => _assets.Destroy(Attackers.gameObject);
	}
}