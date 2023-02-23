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

		public int PlayerId => _playerId;

		public void Construct(Player player) => Player = player;

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