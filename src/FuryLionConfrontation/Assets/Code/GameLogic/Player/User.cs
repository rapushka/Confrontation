using UnityEngine;
using Zenject;

namespace Confrontation
{
	[CreateAssetMenu(fileName = "User", menuName = nameof(Confrontation) + "/User")]
	public class User : ScriptableObject, IInitializable, ILevelSelector
	{
		[SerializeField] private int _playerId = 1;

		public Player Player { get; private set; }

		public ILevel SelectedLevel { get; set; }

		public GameResult GameResult { get; set; } = GameResult.None;

		public void Initialize() => Player = new Player(id: _playerId);

		private void OnEnable() => SelectedLevel = LoadDummyLevel();

		// Instead Level will be null => on game start from GameplayScene will throw exceptions
		private static LevelScriptableObject LoadDummyLevel() => CreateInstance<LevelScriptableObject>();
	}
}