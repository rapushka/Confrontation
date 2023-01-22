using Zenject;

namespace Confrontation
{
	public class UiMediator
	{
		[Inject] private readonly LoadingCurtain _loadingCurtain;
		
		public void ShowLoadingCurtain()            => _loadingCurtain.Show();
		public void ShowImmediatelyLoadingCurtain() => _loadingCurtain.ShowImmediately();
		public void HideLoadingCurtain()            => _loadingCurtain.Hide();
		public void HideImmediatelyLoadingCurtain() => _loadingCurtain.HideImmediately();
	}
}