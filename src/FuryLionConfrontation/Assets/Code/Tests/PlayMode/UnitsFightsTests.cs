using System.Collections;
using System.Linq;
using Confrontation;
using FluentAssertions;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

public class UnitsFightsTests : SceneTestFixture
{
	private readonly WaitForSeconds _waitForZenjectInitialization = new(1f);
	private const int UserPlayerId = 1;
	private bool _isSquadReachTarget;

	[UnityTest]
	public IEnumerator WhenUnitsSquadMovedToVillage_AndUnitsSquadContain1Unit_ThenVillageShouldHave1Unit()
	{
		yield return LoadScene(Constants.SceneName.GameplayScene);
		yield return _waitForZenjectInitialization;

		// Arrange.
		var village = Object.FindObjectsOfType<Village>().First(BelongToEnemy);
		var cellWithVillage = village.RelatedCell;
		var barracks = Object.FindObjectsOfType<Barracks>().First(BelongToEnemy);

		// Act.
		barracks.Action();
		var unitsSquad = barracks.RelatedCell.UnitsSquads!;
		unitsSquad.MoveTo(cellWithVillage);
		yield return new WaitUntil(() => cellWithVillage.HasUnits);

		// Assert.
		var unitsQuantityInVillage = cellWithVillage.UnitsSquads!.QuantityOfUnits;
		unitsQuantityInVillage.Should().Be(1);
	}

	[UnityTest]
	public IEnumerator
		WhenSquadWith1UnitMoveToOtherCell_AndOtherCellHasEnemySquadWith1Unit_ThenCellShouldBecomeNeutral()
	{
		yield return LoadScene(Constants.SceneName.GameplayScene);
		yield return _waitForZenjectInitialization;

		// Arrange.
		var cellWithEnemyVillage = Object.FindObjectsOfType<Village>().Single(BelongToEnemy).RelatedCell;
		
		var enemyBarracks = Object.FindObjectsOfType<Barracks>().First(BelongToEnemy);
		enemyBarracks.Action();
		var enemyUnits = enemyBarracks.RelatedCell.UnitsSquads!;
		
		var friendlyBarracks = Object.FindObjectsOfType<Barracks>().First(BelongToPlayer);
		friendlyBarracks.Action();
		var friendlyUnits = friendlyBarracks.RelatedCell.UnitsSquads!;

		// Act.
		enemyUnits.MoveTo(cellWithEnemyVillage);
		yield return new WaitUntil(() => cellWithEnemyVillage.HasUnits);
		
		friendlyUnits.GetUnitMovement().TargetReached += OnTargetReached;
		friendlyUnits.MoveTo(cellWithEnemyVillage);
		yield return new WaitUntil(() => _isSquadReachTarget);
		_isSquadReachTarget = false;
		friendlyUnits.GetUnitMovement().TargetReached -= OnTargetReached;

		// Assert.
		var owner = cellWithEnemyVillage.RelatedRegion.OwnerPlayerId;
		owner.Should().Be(Constants.NeutralRegion);
	}

	private void OnTargetReached() => _isSquadReachTarget = true;

	private static bool BelongToPlayer(Building building) => building.OwnerPlayerId == UserPlayerId;

	private static bool BelongToEnemy(Building building) => building.OwnerPlayerId != UserPlayerId;
}