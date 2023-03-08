using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameUiMediator
	{
		[Inject] private readonly LoadingCurtain _loadingCurtain;

		[CanBeNull] private RectTransform _canvas;

		public async Task ShowLoadingCurtain() => await _loadingCurtain.Show();
		public async Task HideLoadingCurtain() => await _loadingCurtain.Hide();

		public void ShowImmediatelyLoadingCurtain() => _loadingCurtain.ShowImmediately();
		public void HideImmediatelyLoadingCurtain() => _loadingCurtain.HideImmediately();
	}
}