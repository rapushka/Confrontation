using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class UnitFighter
	{
		private readonly UnitsSquad _squad;
		private readonly IAssetsService _assets;
		private readonly IField _field;

		private int _garrisonQuantity;
		private Garrison _they;
		private Cell _cell;
		[CanBeNull] private Garrison _garrison;

		public UnitFighter(UnitsSquad squad, IAssetsService assets, IField field)
		{
			_squad = squad;
			_assets = assets;
			_field = field;
		}

		private bool IsThereGarrison => _garrison == true;

		private int ComposedDefenceForcesQuantity => _they.QuantityOfUnits + _garrisonQuantity;

		public void FightWithSquadOn(Cell cell)
		{
			_cell = cell;

			_garrison = _field.Garrisons[_cell.Coordinates];
			_garrisonQuantity = IsThereGarrison ? _garrison!.QuantityOfUnits : 0;

			_they = _cell.LocatedUnits!;

			if (IsOurVictory() == false
			    && IsTheirVictory() == false)
			{
				Draw();
			}
		}

		public void CaptureRegion(Cell cell)
		{
			_squad.Coordinates = cell.Coordinates;
			cell.RelatedRegion!.OwnerPlayerId = _squad.OwnerPlayerId;
		}

		private bool IsOurVictory()
		{
			if (_squad.QuantityOfUnits <= ComposedDefenceForcesQuantity)
			{
				return false;
			}

			_squad.QuantityOfUnits -= _they.QuantityOfUnits;

			if (IsThereGarrison)
			{
				_assets.Destroy(_garrison!.gameObject);
			}

			_assets.Destroy(_they.gameObject);
			CaptureRegion(_cell);
			return true;
		}

		private bool IsTheirVictory()
		{
			var ourUnitsQuantity = _squad.QuantityOfUnits;
			if (ourUnitsQuantity >= ComposedDefenceForcesQuantity)
			{
				return false;
			}

			if (IsThereGarrison)
			{
				if (IsDefendersRemain(ourUnitsQuantity) == false)
				{
					return false;
				}
			}
			else
			{
				_they.QuantityOfUnits -= ourUnitsQuantity;
			}

			_assets.Destroy(_squad.gameObject);
			return true;
		}

		private void Draw()
		{
			if (IsThereGarrison)
			{
				_assets.Destroy(_garrison!.gameObject);
			}

			_field.LocatedUnits.Remove(_squad);
			_assets.Destroy(_squad.gameObject);
			_assets.Destroy(_they.gameObject);
			_cell.MakeRegionNeutral();
		}

		private bool IsDefendersRemain(int incomeDamage)
		{
			var remainsDamage = incomeDamage;

			while (remainsDamage > 0)
			{
				var half = remainsDamage / 2;
				var initialTheyQuantity = _they.QuantityOfUnits;
				var initialGarrisonQuantity = _garrison!.QuantityOfUnits;

				_they.QuantityOfUnits -= half;
				_garrison.QuantityOfUnits -= remainsDamage - half;

				TakeDamageOverhead(ref _they, _garrison);
				TakeDamageOverhead(ref _garrison, _they);

				remainsDamage -= CalculateDelta(initialTheyQuantity, initialGarrisonQuantity);
			}

			return _they == true;
		}

		private int CalculateDelta(int initialTheyQuantity, int initialGarrisonQuantity)
		{
			var actualTheyQuantity = _they == true ? _they.QuantityOfUnits : 0;
			var actualGarrisonQuantity = _garrison == true ? _garrison!.QuantityOfUnits : 0;

			return (initialTheyQuantity - actualTheyQuantity)
			       + (initialGarrisonQuantity - actualGarrisonQuantity);
		}

		private void TakeDamageOverhead(ref Garrison left, Garrison right)
		{
			if (left == true)
			{
				left.QuantityOfUnits -= DamageOverhead(right);
			}
		}

		private int DamageOverhead(Garrison garrison)
		{
			var remainQuantity = garrison.QuantityOfUnits;
			if (remainQuantity <= 0)
			{
				_assets.Destroy(garrison.gameObject);
				return Mathf.Abs(remainQuantity);
			}

			return 0;
		}
	}
}