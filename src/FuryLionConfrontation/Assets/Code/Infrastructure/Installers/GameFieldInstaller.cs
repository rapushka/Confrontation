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
			BindInfluences();
		}

		protected abstract void InstallSpecificBindings();

		protected abstract void InstallSpecificFactories();

		private void InstallFactories()
		{
			Container.BindFactory<Cell, Cell.Factory>().FromComponentInNewPrefab(_cellPrefab);
			Container.BindFactory<int, Region, Region.Factory>();
			Container.BindFactory<Building, Building, Building.Factory>().FromFactory<PrefabFactory<Building>>();
		}

		private void BindInfluences()
		{
			Container.BindInterfacesAndSelfTo<InfluenceMediator>().AsSingle();

			Container
				.BindFactory<IInfluencer, OnAllUntilMovingUnitsInfluencer, OnAllUntilMovingUnitsInfluencer.Factory>();
			Container.BindFactory<IInfluencer, OnAllMovingUnitsInfluencer, OnAllMovingUnitsInfluencer.Factory>();
			Container.BindFactory<IInfluencer, OnOurUnitsInfluencer, OnOurUnitsInfluencer.Factory>();
			Container.BindFactory<float, IInfluencer, DuratedInfluencer, DuratedInfluencer.Factory>();
			Container.BindFactory<Influence, InfluencerBase, InfluencerBase.Factory>();
			Container.BindFactory<IInfluencer, PermanentInfluencer, PermanentInfluencer.Factory>();
			Container.BindFactory<IInfluencer, OnOurForgesInfluencer, OnOurForgesInfluencer.Factory>();
			Container.BindFactory<IInfluencer, OnOurFarmsInfluencer, OnOurFarmsInfluencer.Factory>();
			Container.BindFactory<IInfluencer, OnOurGoldenMinesInfluencer, OnOurGoldenMinesInfluencer.Factory>();
		}
	}
}