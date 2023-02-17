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
		private UnitsSquad _they;
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
				DistributeDamage(ourUnitsQuantity);
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

		private void DistributeDamage(int ourUnitsQuantity)
		{
			var distributedDamage = ourUnitsQuantity / 2;
			_they.QuantityOfUnits -= distributedDamage;

			if (_they.QuantityOfUnits <= 0)
			{
				_garrison!.QuantityOfUnits -= Mathf.Abs(_they.QuantityOfUnits);
				_assets.Destroy(_they.gameObject);
				_cell.MakeRegionNeutral();
			}

			_garrison!.QuantityOfUnits -= ourUnitsQuantity - distributedDamage;

			if (_garrison!.QuantityOfUnits <= 0)
			{
				_they.QuantityOfUnits -= Mathf.Abs(_garrison!.QuantityOfUnits);
				_assets.Destroy(_garrison!.gameObject);
			}
		}
	}
}