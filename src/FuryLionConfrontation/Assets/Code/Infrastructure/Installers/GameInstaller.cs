using Confrontation.GameLogic;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private LoadingCurtain _loadingCurtainPrefab;
		[SerializeField] private User _user;
		[SerializeField] private ResourcesService _resources;
		[SerializeField] private BalanceTable _balanceTable;

		public override void InstallBindings()
		{
			BindPrefabs();

			BindTimeServices();

			Container.BindInterfacesTo<InputService>().AsSingle();
			Container.BindInterfacesTo<UniTaskRunnerService>().FromNewComponentOnNewGameObject().AsSingle();

			Container.BindInterfacesTo<AssetsService>().AsSingle();
			Container.BindInterfacesTo<SceneTransferService>().AsSingle();

			Container.Bind<GameUiMediator>().AsSingle();

			Container.Bind<ToMainMenu>().AsSingle();
			Container.Bind<ToGameplay>().AsSingle();
			Container.Bind<ToLevelEditor>().AsSingle();

			StartGame();
		}

		private void BindPrefabs()
		{
			Container.BindInstance<IBalanceTable>(_balanceTable).AsSingle();
			Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtainPrefab).AsSingle();
			Container.BindInterfacesAndSelfTo<User>().FromInstance(_user).AsSingle();
			Container.BindInstance<IResourcesService>(_resources).AsSingle();
		}

		private void BindTimeServices()
		{
			Container.Bind<ITimeService>().To<TimeService>().AsSingle().WhenInjectedInto<TimeStopService>();
			Container.Bind<ITimeService>().To<TimeStopService>().FromResolve().WhenInjectedInto<TimeServiceAccelerator>();

			Container.Bind(typeof(TimeStopService))
			         .To<TimeStopService>()
			         .AsSingle();

			Container.Bind(typeof(ITimeService), typeof(TimeServiceAccelerator), typeof(IInitializable))
			         .To<TimeServiceAccelerator>()
			         .AsSingle();
		}

		private void StartGame() => Container.BindInterfacesTo<ToBootstrapOnInitialize>().AsSingle();
	}
}