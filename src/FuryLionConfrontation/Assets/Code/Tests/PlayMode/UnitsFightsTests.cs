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
		private const int EnemyPlayerId = 2;
		private readonly WaitForSeconds _waitForZenjectInitialization = new(seconds: 0.1f);

		private int _initialVillageGarrisonAmount;

		private DiContainer _container;
		private List<Building> _buildings;

		private VillageLevelStats FirstLevelVillage => _container.Resolve<IBalanceTable>().VillageStats.LeveledStats[1];

		public override void SetUp()
		{
			base.SetUp();
			Time.timeScale = 10f;
		}

		public override void Teardown()
		{
			base.Teardown();

			SetVillageGarrisonAmount(to: _initialVillageGarrisonAmount);
		}

		private IEnumerator CommonSetUp()
		{
			yield return LoadScene(Constants.SceneName.MainMenuScene);
			yield return _waitForZenjectInitialization;

			yield return PassMainMenu();

			_container = GetActualContainer(@for: Constants.SceneName.GameplayScene);
			PreventGarrisonSpawn();
			_buildings = _container.ResolveBuildings();
		}

		private void PreventGarrisonSpawn()
		{
			_initialVillageGarrisonAmount = FirstLevelVillage.Amount;
			SetVillageGarrisonAmount(to: 0);
		}

		private void SetVillageGarrisonAmount(int to) => FirstLevelVillage.SetGenerationAmount(to);

		private object PassMainMenu()
		{
			var container = GetActualContainer(@for: Constants.SceneName.MainMenuScene);

			var user = container.Resolve<User>();
			user.SelectedLevel = Resources.Load<LevelScriptableObject>(Constants.ResourcePath.TestLevel);

			var toGameplay = container.Resolve<ToGameplay>();
			toGameplay.Transfer();

			return _waitForZenjectInitialization;
		}

		private static DiContainer GetActualContainer(string @for)
			=> ProjectContext.Instance.Container.Resolve<SceneContextRegistry>()
			                 .TryGetSceneContextForScene(@for)
			                 .Container;

		[UnityTest]
		public IEnumerator _0_WhenSceneLoaded_AndResolveVillages_ThenShouldNotContainUnityNulls()
		{
			yield return CommonSetUp();

			// Assert.
			var countOfNullBuildings = _buildings.Count((b) => b == false);
			countOfNullBuildings.Should().Be(0);
		}

		[UnityTest]
		public IEnumerator _1_WhenUnitsSquadMovedToVillage_AndUnitsSquadContain1Unit_ThenVillageShouldHave1Unit()
		{
			yield return CommonSetUp();

			// Arrange.
			const int quantityToMove = 1;
			var units = Spawn.Units(_buildings, that: BelongToEnemy);
			var cellWithVillage = _buildings.OfType<Village>().First(BelongToEnemy).RelatedCell;

			// Act.
			units.MoveTo(cellWithVillage, quantityToMove);
			yield return units.WaitForTargetReach();

			// Assert.
			var unitsQuantityInVillage = cellWithVillage.LocatedUnits!.QuantityOfUnits;
			unitsQuantityInVillage.Should().Be(quantityToMove);
		}

		[UnityTest]
		public IEnumerator
			_2_WhenSquadWith1UnitMoveToOtherCell_AndOtherCellHasEnemySquadWith1Unit_ThenCellShouldBecomeNeutral()
		{
			yield return CommonSetUp();

			// Arrange.
			const int userQuantity = 1;
			const int enemyQuantity = 1;
			var cellWithEnemyVillage = _buildings.OfType<Village>().First(BelongToEnemy).RelatedCell;

			var enemyUnits = Spawn.Units(_buildings, that: BelongToEnemy);
			var friendlyUnits = Spawn.Units(_buildings, that: BelongToPlayer);

			// Act.
			enemyUnits.MoveTo(cellWithEnemyVillage, enemyQuantity);
			yield return enemyUnits.WaitForTargetReach();

			friendlyUnits.MoveTo(cellWithEnemyVillage, userQuantity);
			yield return friendlyUnits.WaitForTargetReach();

			// Assert.
			var owner = cellWithEnemyVillage.OwnerPlayerId;
			owner.Should().Be(Constants.NeutralRegion);
		}

		[UnityTest]
		public IEnumerator
			_3_WhenSquadWith1UnitMoveToOtherCell_AndOtherCellHasEnemySquadWith2Units_ThenCellOwnerShouldStaySame()
		{
			yield return CommonSetUp();

			// Arrange.
			const int userQuantity = 1;
			const int enemyQuantity = 2;
			var cellWithEnemyVillage = _buildings.OfType<Village>().First(BelongToEnemy).RelatedCell;

			var enemyUnits = Spawn.Units(_buildings, that: BelongToEnemy, quantity: enemyQuantity);
			var friendlyUnits = Spawn.Units(_buildings, that: BelongToPlayer);
			var initialOwner = cellWithEnemyVillage.OwnerPlayerId;

			// Act.
			enemyUnits.MoveTo(cellWithEnemyVillage, enemyQuantity);
			yield return enemyUnits.WaitForTargetReach();

			friendlyUnits.MoveTo(cellWithEnemyVillage, userQuantity);
			yield return friendlyUnits.WaitForTargetReach();

			// Assert.
			var owner = cellWithEnemyVillage.OwnerPlayerId;
			owner.Should().Be(initialOwner);
		}

		[UnityTest]
		public IEnumerator
			_4_WhenSquadWith2UnitMoveToOtherCell_AndOtherCellHasEnemySquadWith1Unit_ThenCellOwnerShouldBecomeToUser()
		{
			yield return CommonSetUp();

			// Arrange.
			const int userQuantity = 2;
			const int enemyQuantity = 1;
			var cellWithEnemyVillage = _buildings.OfType<Village>().First(BelongToEnemy).RelatedCell;

			var enemyUnits = Spawn.Units(_buildings, that: BelongToEnemy);
			var friendlyUnits = Spawn.Units(_buildings, that: BelongToPlayer, quantity: userQuantity);

			// Act.
			enemyUnits.MoveTo(cellWithEnemyVillage, enemyQuantity);
			yield return enemyUnits.WaitForTargetReach();

			friendlyUnits.MoveTo(cellWithEnemyVillage, userQuantity);
			yield return friendlyUnits.WaitForTargetReach();

			// Assert.
			var owner = cellWithEnemyVillage.OwnerPlayerId;
			owner.Should().Be(UserPlayerId);
		}

		[UnityTest]
		public IEnumerator _5_WhenSquadWith2UnitsMoveToOtherCell_AndTheyStartFromVillage_ThenSend1UnitAndLeave1Unit()
		{
			yield return CommonSetUp();

			// Arrange.
			const int quantityToSpawn = 2;
			const int quantityToSend = 1;

			var friendlyUnits = Spawn.Units(_buildings, that: BelongToPlayer, quantity: quantityToSpawn);
			var cellWithVillage = _buildings.OfType<Village>().First(BelongToPlayer).RelatedCell;
			var otherCell = _buildings.OfType<Village>().First(BelongToEnemy).RelatedCell;

			// Act.
			friendlyUnits.MoveTo(cellWithVillage, quantityToSpawn);
			yield return friendlyUnits.WaitForTargetReach();

			friendlyUnits.MoveTo(otherCell, quantityToSend);
			yield return friendlyUnits.WaitForTargetReach();

			// Assert.
			cellWithVillage.LocatedUnits!.QuantityOfUnits.Should().Be(1);
			otherCell.LocatedUnits!.QuantityOfUnits.Should().Be(1);
		}

		[UnityTest]
		public IEnumerator
			_6_WhenFriendlySquadCaptureEnemyCell_AndRecentlyEnemyUnitsMoveToOurVillage_ThenVillageShouldStayFriendly()
		{
			yield return CommonSetUp();

			// Arrange.
			const int userQuantity = 1;
			const int enemyQuantity = 1;
			var cellWithEnemyVillage = _buildings.OfType<Village>().First(BelongToEnemy).RelatedCell;
			var cellWithFriendlyVillage = _buildings.OfType<Village>().First(BelongToPlayer).RelatedCell;

			var friendlyUnits = Spawn.Units(_buildings, that: BelongToPlayer, quantity: userQuantity);
			var enemyUnits = Spawn.Units(_buildings, that: BelongToEnemy, quantity: enemyQuantity);

			// Act.
			friendlyUnits.MoveTo(cellWithEnemyVillage, userQuantity);
			yield return friendlyUnits.WaitForTargetReach();

			enemyUnits.MoveTo(cellWithFriendlyVillage, enemyQuantity);
			yield return enemyUnits.WaitForTargetReach();

			// Assert.
			var owner = cellWithFriendlyVillage.OwnerPlayerId;
			owner.Should().Be(UserPlayerId);
		}

		private static bool BelongToPlayer(Building building) => building.RelatedCell.OwnerPlayerId == UserPlayerId;

		private static bool BelongToEnemy(Building building) => building.RelatedCell.OwnerPlayerId == EnemyPlayerId;
	}
}