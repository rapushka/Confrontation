using System.Collections;
using Confrontation;
using Confrontation.Editor;
using UnityEngine;

public static class UnitsSquadExtensions
{
	private static bool _isSquadReachTarget;

	public static IEnumerator WaitForTargetReach(this UnitsSquad @this)
	{
		@this.GetUnitMovement().TargetReached += OnTargetReached;
		yield return new WaitUntil(() => _isSquadReachTarget);
		_isSquadReachTarget = false;
		@this.GetUnitMovement().TargetReached -= OnTargetReached;
	}

	public static UnitMovement GetUnitMovement(this UnitsSquad @this)
		=> @this.GetPrivateField<UnitMovement>("_unitMovement");

	private static void OnTargetReached() => _isSquadReachTarget = true;

}