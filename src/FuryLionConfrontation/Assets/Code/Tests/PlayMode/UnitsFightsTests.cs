using System.Collections;
using System.Linq;
using FluentAssertions;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation.Editor.PlayModeTests
{
	public class UnitsFightsTests : SceneTestFixture
	{
		private readonly WaitForSeconds _waitForZenjectInitialization = new(seconds: 1f);
		private const int UserPlayerId = 1;
		private bool _isSquadReachTarget;

		private IEnumerator CommonSetUp()
		{
			yield return LoadScene(Constants.SceneName.GameplayScene);
			yield return _waitForZenjectInitialization;
		}

		[UnityTest]
		public IEnumerator _0_WhenSceneLoaded_AndResolveVillages_ThenShouldNotContainNulls()
		{
			yield return CommonSetUp();

			var context = ProjectContext.Instance.Container.Resolve<SceneContextRegistry>()
			                                               .TryGetSceneContextForScene(Constants.SceneName.GameplayScene);

			var buildings = context.Container.Resolve<BuildingsGenerator>().Buildings;

			buildings.Count((b) => b == true).Should().Be(buildings.Count);
		}

		[UnityTest]
		public IEnumerator _1_WhenUnitsSquadMovedToVillage_AndUnitsSquadContain1Unit_ThenVillageShouldHave1Unit()
		{
			yield return CommonSetUp();

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
			_2_WhenSquadWith1UnitMoveToOtherCell_AndOtherCellHasEnemySquadWith1Unit_ThenCellShouldBecomeNeutral()
		{
			yield return CommonSetUp();

			// Arrange.
			var buildings = SceneContainer.Resolve<BuildingsGenerator>().Buildings;
			var cellWithEnemyVillage = buildings.OfType<Village>().First(BelongToEnemy).RelatedCell;

			var enemyUnits = Spawn.Units(buildings, BelongToEnemy);
			var friendlyUnits = Spawn.Units(buildings, BelongToPlayer);

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
}