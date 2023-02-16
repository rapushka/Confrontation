namespace Confrontation
{
	public class UnitFighter
	{
		private readonly UnitsSquad _squad;
		private readonly IAssetsService _assets;
		private readonly IField _field;

		public UnitFighter(UnitsSquad squad, IAssetsService assets, IField field)
		{
			_squad = squad;
			_assets = assets;
			_field = field;
		}

		public void FightWithSquadOn(Cell cell)
		{
			var enemies = cell.LocatedUnits!;

			if (IsOurVictory(cell, enemies) == false
			    && IsTheirVictory(enemies) == false)
			{
				IsDraw(cell, enemies);
			}
		}

		private bool IsOurVictory(Cell cell, Garrison they)
		{
			if (_squad.QuantityOfUnits > they.QuantityOfUnits)
			{
				_squad.QuantityOfUnits -= they.QuantityOfUnits;
				_assets.Destroy(they.gameObject);
				CaptureRegion(cell);
				return true;
			}

			return false;
		}

		private bool IsTheirVictory(Garrison they)
		{
			if (_squad.QuantityOfUnits < they.QuantityOfUnits)
			{
				they.QuantityOfUnits -= _squad.QuantityOfUnits;
				_assets.Destroy(_squad.gameObject);
				return true;
			}

			return false;
		}

		private void IsDraw(Cell cell, Garrison they)
		{
			if (_squad.QuantityOfUnits == they.QuantityOfUnits)
			{
				_field.LocatedUnits.Remove(_squad);
				_assets.Destroy(_squad.gameObject);
				_assets.Destroy(they.gameObject);
				cell.MakeRegionNeutral();
			}
		}

		public void CaptureRegion(Cell cell)
		{
			_squad.Coordinates = cell.Coordinates;
			cell.RelatedRegion!.OwnerPlayerId = _squad.OwnerPlayerId;
		}
	}
}