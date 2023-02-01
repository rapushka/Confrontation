using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Confrontation.Editor.PlayModeTests
{
	public class UnitsFightsTests : SceneTestFixture
	{
		private const int UserPlayerId = 1;
		private readonly WaitForSeconds _waitForZenjectInitialization = new(seconds: 1f);

		private DiContainer _container;
		private List<Building> _buildings;

		private IEnumerator CommonSetUp()
		{
			yield return LoadScene(Constants.SceneName.GameplayScene);
			yield return _waitForZenjectInitialization;

			_container = GetActualContainer();
			_buildings = Resolve.Buildings(_container);
		}

		private static DiContainer GetActualContainer()
			=> ProjectContext.Instance.Container.Resolve<SceneContextRegistry>()
			                 .TryGetSceneContextForScene(Constants.SceneName.GameplayScene)
			                 .Container;

		[UnityTest]
		public IEnumerator _0_WhenSceneLoaded_AndResolveVillages_ThenShouldNotContainUnityNulls()
		{
			yield return CommonSetUp();

			// Assert.
			_buildings.Count((b) => b == true).Should().Be(_buildings.Count);
		}

		[UnityTest]
		public IEnumerator _1_WhenUnitsSquadMovedToVillage_AndUnitsSquadContain1Unit_ThenVillageShouldHave1Unit()
		{
			yield return CommonSetUp();

			// Arrange.
			const int quantityToMove = 1;
			var units = Spawn.Units(_buildings, BelongToEnemy);
			var cellWithVillage = _buildings.OfType<Village>().First(BelongToEnemy).RelatedCell;

			// Act.
			units.MoveTo(cellWithVillage, quantityToMove);
			yield return new WaitUntil(() => cellWithVillage.HasUnits);

			// Assert.
			var unitsQuantityInVillage = cellWithVillage.UnitsSquads!.QuantityOfUnits;
			unitsQuantityInVillage.Should().Be(quantityToMove);
		}

		[UnityTest]
		public IEnumerator
			_2_WhenSquadWith1UnitMoveToOtherCell_AndOtherCellHasEnemySquadWith1Unit_ThenCellShouldBecomeNeutral()
		{
			yield return CommonSetUp();

			// Arrange.
			const int quantityToMove = 1;
			var cellWithEnemyVillage = _buildings.OfType<Village>().First(BelongToEnemy).RelatedCell;

			var enemyUnits = Spawn.Units(_buildings, BelongToEnemy);
			var friendlyUnits = Spawn.Units(_buildings, BelongToPlayer);

			// Act.
			enemyUnits.MoveTo(cellWithEnemyVillage, quantityToMove);
			yield return new WaitUntil(() => cellWithEnemyVillage.HasUnits);

			friendlyUnits.MoveTo(cellWithEnemyVillage, quantityToMove);
			yield return friendlyUnits.WaitForTargetReach();

			// Assert.
			var owner = cellWithEnemyVillage.RelatedRegion.OwnerPlayerId;
			owner.Should().Be(Constants.NeutralRegion);
		}

		private static bool BelongToPlayer(Building building) => building.OwnerPlayerId == UserPlayerId;

		private static bool BelongToEnemy(Building building) => building.OwnerPlayerId != UserPlayerId;
	}
}