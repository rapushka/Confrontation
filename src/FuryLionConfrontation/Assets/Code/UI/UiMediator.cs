using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class UiMediator
	{
		[Inject] private readonly LoadingCurtain _loadingCurtain;
		[Inject] private readonly Windows _windows;
		[Inject] private readonly IAssetsService _assets;
		[Inject] private readonly RectTransform _canvasPrefab;

		[CanBeNull] private RectTransform _canvas;

		public RectTransform Canvas => _canvas ??= _assets.Instantiate(_canvasPrefab);

		public void ShowLoadingCurtain()                 => _loadingCurtain.Show();
		public void ShowImmediatelyLoadingCurtain()      => _loadingCurtain.ShowImmediately();
		public void HideLoadingCurtain()                 => _loadingCurtain.Hide();
		public void HideImmediatelyLoadingCurtain()      => _loadingCurtain.HideImmediately();
		public void ShowWindow<T>() where T : WindowBase => _windows.Show<T>();
		public void HideWindow()                         => _windows.Hide();
		public void Build(Building building)             => Debug.Log("In this moment will be created building:)");
	}
}