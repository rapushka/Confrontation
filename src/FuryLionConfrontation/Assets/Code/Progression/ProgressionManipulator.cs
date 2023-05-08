using Zenject;

namespace Confrontation
{
	public class ProgressionManipulator
	{
		[Inject] private readonly IProgressionStorageService _progression;

		private static PlayerProgress EmptyAccount
			=> new()
			{
				KalymCount = 0,
				CompletedLevelsCount = 0,
				LearnedSpellsCount = 0,
			};

		private static PlayerProgress CompletedAccount
			=> new()
			{
				KalymCount = 99_999,
				CompletedLevelsCount = 999,
				LearnedSpellsCount = 999,
			};

		public void UnlockAll() => _progression.SaveProgress(CompletedAccount);

		public void Reset() => _progression.SaveProgress(EmptyAccount);
	}
}