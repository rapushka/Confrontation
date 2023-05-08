using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private LoadingCurtain _loadingCurtainPrefab;
		[SerializeField] private User _user;
		[SerializeField] private ResourcesService _resources;
		[SerializeField] private StatsTable _statsTable;

		public override void InstallBindings()
		{
			BindPrefabs();

			DecorateStatsTable();
			DecorateTimeService();

			Container.BindInterfacesTo<InputService>().AsSingle();
			Container.BindInterfacesTo<UniTaskRunnerService>().FromNewComponentOnNewGameObject().AsSingle();

			Container.BindInterfacesTo<AssetsService>().AsSingle();
			Container.BindInterfacesTo<SceneTransferService>().AsSingle();

			Container.Bind<GameUiMediator>().AsSingle();

			Container.Bind<ToMainMenu>().AsSingle();
			Container.Bind<ToGameplay>().AsSingle();
			Container.Bind<ToLevelEditor>().AsSingle();
			Container.Bind<Progression>().AsSingle();

			Container.Bind<IProgressionStorageService>().To<PlayerPrefsProgressionService>().AsSingle();

			StartGame();
		}

		private void BindPrefabs()
		{
			Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtainPrefab).AsSingle();
			Container.BindInterfacesAndSelfTo<User>().FromInstance(_user).AsSingle();
			Container.BindInstance<IResourcesService>(_resources).AsSingle();
		}

		private void DecorateTimeService()
		{
			Container.BindSelf<TimeService>().AsSingle();
			Container.BindSelf<TimeAccelerationService>().AsSingle();
			Container.BindSelf<TimeStopService>().AsSingle();

			Container.DecorateFromResolve<ITimeService, TimeService, TimeAccelerationService>();
			Container.DecorateFromResolve<ITimeService, TimeAccelerationService, TimeStopService>();
			Container.Bind<ITimeService>().To<TimeStopService>().FromResolve();

			Container.Bind<IInitializable>().To<TimeAccelerationService>().FromResolve();
		}

		private void DecorateStatsTable()
		{
			Container.BindSelf<StatsTable>().FromInstance(_statsTable).AsSingle();
			Container.Bind<IStatsTable>().To<StatsTable>().FromResolve();
		}

		private void StartGame() => Container.BindInterfacesTo<ToBootstrapOnInitialize>().AsSingle();
	}
}