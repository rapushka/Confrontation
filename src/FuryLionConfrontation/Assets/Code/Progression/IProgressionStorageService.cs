namespace Confrontation
{
	public interface IProgressionStorageService
	{
		PlayerProgress LoadProgress();

		void SaveProgress(PlayerProgress progress);
	}
}