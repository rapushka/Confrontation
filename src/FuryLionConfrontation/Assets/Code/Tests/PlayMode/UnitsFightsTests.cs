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
		yield return new WaitUntil(() => cellWithVillage.UnitsSquads == true);

		// Assert.
		var unitsQuantity = cellWithVillage.UnitsSquads!.QuantityOfUnits;
		unitsQuantity.Should().Be(1);
	}

	private static bool BelongToEnemy(Building building) => building.OwnerPlayerId != UserPlayerId;
}