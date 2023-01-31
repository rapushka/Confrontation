using System;

namespace Confrontation.Editor
{
	public static class UnitOrderPerformerExtensions
	{
		private const string TargetCellFieldName = "_targetCell";
		private const string UnitsSquadFieldName = "_unitsSquad";

		public static void UpdateTargetCell(this UnitOrderPerformer @this, Func<Cell, Cell> via)
			=> @this.SetTargetCell(via.Invoke(@this.GetTargetCell()));
		
		public static Cell GetTargetCell(this UnitOrderPerformer @this)
			=> @this.GetPrivateField<Cell>(TargetCellFieldName);

		public static void SetTargetCell(this UnitOrderPerformer @this, Cell value)
			=> @this.SetPrivateField(TargetCellFieldName, value);

		public static int GetQuantityOfUnits(this UnitOrderPerformer @this)
			=> @this.GetPrivateField<UnitsSquad>(UnitsSquadFieldName).QuantityOfUnits;
	}
}