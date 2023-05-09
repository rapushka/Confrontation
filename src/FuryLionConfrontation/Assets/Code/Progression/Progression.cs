using System;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Confrontation
{
	public class Progression
	{
		[Inject] private readonly IProgressionStorageService _progressionStorage;
		[Inject] private readonly User _user;
		[Inject] private readonly IStatsTable _stats;

		public event Action KalymValueChanged;

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

		public void SpentPlayerKalym(int value)
		{
			Assert.IsTrue(value > 0);

			UpdateProgress(with: (p) => p.KalymCount -= value);
			KalymValueChanged?.Invoke();
		}

		private void AddKalymToPlayer()
		{
			UpdateProgress(with: (p) => p.KalymCount += LevelReward);
			KalymValueChanged?.Invoke();
		}

		private void OpenNewLevel() => UpdateProgress(with: (p) => p.CompletedLevelsCount++);

		private void UpdateProgress(Action<PlayerProgress> with)
			=> _progressionStorage.SaveProgress(CurrentPlayer.With(with));
	}
}