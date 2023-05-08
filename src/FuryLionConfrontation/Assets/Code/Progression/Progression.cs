using System;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Progression
	{
		[Inject] private readonly IProgressionStorageService _progressionStorage;
		[Inject] private readonly User _user;
		[Inject] private readonly IStatsTable _stats;

		private PlayerProgress CurrentPlayer => _progressionStorage.LoadProgress();

		private int CompletedLevel => _user.SelectedLevelNumber;

		private bool IsLastLevelCompleted => CompletedLevel == CurrentPlayer.CompletedLevelsCount + 1;

		private int LevelNumberBonus => Mathf.RoundToInt(CompletedLevel * ProgressionStats.LevelNumberMultiplier);

		private UserProgressionStats ProgressionStats => _stats.UserProgressionStats;

		private int LevelReward => ProgressionStats.KalymPerLevel + LevelNumberBonus;

		public void LevelCompleted()
		{
			AddKalymToPlayer();

			if (IsLastLevelCompleted)
			{
				OpenNewLevel();
			}
		}

		private void AddKalymToPlayer() => UpdateProgress(with: (p) => p.KalymCount += LevelReward);

		private void OpenNewLevel() => UpdateProgress(with: (p) => p.CompletedLevelsCount++);

		private void UpdateProgress(Action<PlayerProgress> with)
			=> _progressionStorage.SaveProgress(CurrentPlayer.With(with));
	}
}