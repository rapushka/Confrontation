using Zenject;

namespace Confrontation
{
	public class UiMediator
	{
		[Inject] private readonly LoadingCurtain _loadingCurtain;
		[Inject] private readonly Windows _windows;

		public void ShowLoadingCurtain()                 => _loadingCurtain.Show();
		public void ShowImmediatelyLoadingCurtain()      => _loadingCurtain.ShowImmediately();
		public void HideLoadingCurtain()                 => _loadingCurtain.Hide();
		public void HideImmediatelyLoadingCurtain()      => _loadingCurtain.HideImmediately();
		public void ShowWindow<T>() where T : WindowBase => _windows.Show<T>();
		public void HideWindow()                         => _windows.Hide();
	}
}