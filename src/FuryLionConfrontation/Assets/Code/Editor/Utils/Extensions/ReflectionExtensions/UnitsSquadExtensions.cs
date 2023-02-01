using Confrontation;
using Confrontation.Editor;

public static class UnitsSquadExtensions
{
	public static Cell GetLocationCell(this UnitsSquad @this)
		=> @this.GetPrivateField<UnitOrderPerformer>("_unitOrderPerformer").GetPrivateField<Cell>("_locationCell");

	public static UnitMovement GetUnitMovement(this UnitsSquad @this)
		=> @this.GetPrivateField<UnitMovement>("_unitMovement");
}