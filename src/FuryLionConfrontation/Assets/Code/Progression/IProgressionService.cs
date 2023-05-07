namespace Confrontation
{
	public interface IProgressionService
	{
		PlayerProgress LoadProgress();

		void SaveProgress(PlayerProgress progress);
	}
}