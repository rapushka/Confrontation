using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameUiMediator
	{
		[Inject] private readonly LoadingCurtain _loadingCurtain;

		[CanBeNull] private RectTransform _canvas;

		public void ShowLoadingCurtain()            => _loadingCurtain.Show();
		public void ShowImmediatelyLoadingCurtain() => _loadingCurtain.ShowImmediately();
		public void HideLoadingCurtain()            => _loadingCurtain.Hide();
		public void HideImmediatelyLoadingCurtain() => _loadingCurtain.HideImmediately();
	}
}