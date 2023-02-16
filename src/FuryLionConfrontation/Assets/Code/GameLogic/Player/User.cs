using System;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = "User", menuName = nameof(Confrontation) + "/User")]
	public class User : ScriptableObject, ILevelSelector
	{
		[SerializeField] private int _playerId = 1;

		public Player Player { get; private set; }

		public ILevel SelectedLevel { get; set; }

		public GameResult GameResult { get; set; } = GameResult.None;

		public void InitializePlayer(Func<int, Player> initializer) => Player = initializer.Invoke(_playerId);

		private void OnEnable() => SelectedLevel = LoadDummyLevel();

		// Instead Level will be null => on game start from GameplayScene will throw exceptions
		private LevelScriptableObject LoadDummyLevel()
		{
			var level = CreateInstance<LevelScriptableObject>();
			level.Regions.Add(new Region.Data { OwnerPlayerId = _playerId });
			return level;
		}
	}
}