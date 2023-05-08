using UnityEngine;

namespace Confrontation
{
	public class PlayerPrefsProgressionService : IProgressionStorageService
	{
		private const string CompletedLevelsCount = nameof(PlayerProgress.CompletedLevelsCount);
		private const string KalymCount = nameof(PlayerProgress.KalymCount);
		private const string LearnedSpellsCount = nameof(PlayerProgress.LearnedSpellsCount);

		public PlayerProgress LoadProgress()
			=> new()
			{
				CompletedLevelsCount = PlayerPrefs.GetInt(CompletedLevelsCount),
				KalymCount = PlayerPrefs.GetInt(KalymCount),
				LearnedSpellsCount = PlayerPrefs.GetInt(LearnedSpellsCount),
			};

		public void SaveProgress(PlayerProgress progress)
		{
			PlayerPrefs.SetInt(CompletedLevelsCount, progress.CompletedLevelsCount);
			PlayerPrefs.SetInt(KalymCount, progress.KalymCount);
			PlayerPrefs.SetInt(LearnedSpellsCount, progress.LearnedSpellsCount);
		}
	}
}