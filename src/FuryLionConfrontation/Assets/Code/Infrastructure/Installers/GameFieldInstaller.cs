using UnityEngine;
using Zenject;

namespace Confrontation
{
	public abstract class GameFieldInstaller : MonoInstaller<GameplayInstaller>
	{
		[SerializeField] private Cell _cellPrefab;
		[SerializeField] private BackToMenuButton _backToMenuButton;
		[SerializeField] private RectTransform _canvas;
		[SerializeField] private WindowsContainer _windowsContainer;

		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<WindowsContainer>().FromInstance(_windowsContainer).AsSingle();
			Container.BindInstance(_canvas).AsSingle();
			Container.BindInstance(_backToMenuButton).AsSingle();

			InstallSpecificBindings();
			InstallFactories();
			InstallSpecificFactories();
		}

		protected abstract void InstallSpecificBindings();

		protected abstract void InstallSpecificFactories();

		private void InstallFactories()
		{
			Container.BindFactory<Cell, Cell.Factory>().FromComponentInNewPrefab(_cellPrefab);
			Container.BindFactory<int, Region, Region.Factory>();
			Container.BindFactory<Building, Building, Building.Factory>().FromFactory<PrefabFactory<Building>>();
		}
	}
}