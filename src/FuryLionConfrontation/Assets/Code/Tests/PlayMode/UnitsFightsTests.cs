using System.Collections;
using System.Linq;
using Confrontation;
using FluentAssertions;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

public class UnitsFightsTests : SceneTestFixture
{
	private const int UserPlayerId = 1;

	[UnityTest]
	public IEnumerator
		WhenSquadWith1UnitMoveToOtherCell_AndOnOtherCellIsEnemySquadWith1Unit_ThenCellShouldBecomeNeutral()
	{
		yield return LoadScene(Constants.SceneName.GameplayScene);

		// Arrange.
		var enemyVillage = Object.FindObjectsOfType<Village>().Single((v) => v.OwnerPlayerId != UserPlayerId);
		var cell = enemyVillage.RelatedCell;
		

		// Act.

		// Assert.
		var owner = cell.RelatedRegion.OwnerPlayerId;
		owner.Should().Be(Constants.NeutralRegion);
	}
}