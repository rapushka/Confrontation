using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Progression
	{
		[Inject] private readonly IProgressionStorageService _progressionStorage;
		[Inject] private readonly User _user;

		private PlayerProgress CurrentPlayer => _progressionStorage.LoadProgress();

		private int CompletedLevel => _user.SelectedLevelNumber;

		private bool IsLastLevelCompleted => CompletedLevel == CurrentPlayer.CompletedLevelsCount + 1;

		public void LevelCompleted()
		{
			if (IsLastLevelCompleted)
			{
				var playerProgress = CurrentPlayer;
				playerProgress.CompletedLevelsCount++;
				_progressionStorage.SaveProgress(playerProgress);
			}
		}
	}
}