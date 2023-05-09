using JetBrains.Annotations;
using Zenject;

namespace Confrontation
{
	public class UnitFighter
	{
		[Inject] private readonly IAssetsService _assets;
		[Inject] private readonly UnitsSquad _attackers;
		[Inject] private readonly DefenceStrategyFactory _defenceStrategyFactory;
		[Inject] private readonly ISoundService _playSound;

		[CanBeNull] private Garrison _garrison;
		private Cell _cell;
		private (float Attackers, float Defenders) _cachedSidesHealth;
		private IDefenceStrategy _defenders;

		private bool IsDefendersAlive => _defenders.QuantityOfUnits > 0;

		private bool IsAttackersAlive => _attackers.QuantityOfUnits > 0;

		public void CaptureRegion(Cell cell)
		{
			_attackers.Coordinates = cell.Coordinates;
			cell.RelatedRegion!.OwnerPlayerId = _attackers.OwnerPlayerId;
		}

		public void FightWithSquadOn(Cell cell)
		{
			_playSound.UnitsFight(Constants.Audio.VolumeScale.Enemy);
			_cell = cell;
			_defenders = PickDefenceStrategy(_cell);

			FightToDeath();
			DetermineWinner();
		}

		private void FightToDeath()
		{
			while (IsAttackersAlive && IsDefendersAlive)
			{
				if (_attackers.HealthPoints.IsEqualFloats(_cachedSidesHealth.Attackers)
				    && _defenders.HealthPoints.IsEqualFloats(_cachedSidesHealth.Defenders))
				{
					KillBoth();
					break;
				}

				_cachedSidesHealth = (_attackers.HealthPoints, _defenders.HealthPoints);

				var defendersDamage = _defenders.BaseDamage;
				var attackersDamage = _attackers.AttackDamage;

				_attackers.Health.TakeDamage(defendersDamage);
				_defenders.TakeDamageOnDefence(attackersDamage, _attackers.DefencePierceRate);
			}
		}

		private void KillBoth()
		{
			_defenders.Kill();
			_attackers.Kill();
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

		private IDefenceStrategy PickDefenceStrategy(Cell cell) => _defenceStrategyFactory.Create(cell);

		private void AttackersWin()
		{
			_defenders.Destroy();

			CaptureRegion(_cell);
		}

		private void DefendersWin() => DestroyAttackers();

		private void Draw()
		{
			_defenders.Destroy();
			DestroyAttackers();

			_cell.MakeRegionNeutral();
		}

		private void DestroyAttackers() => _assets.Destroy(_attackers.gameObject);
	}
}