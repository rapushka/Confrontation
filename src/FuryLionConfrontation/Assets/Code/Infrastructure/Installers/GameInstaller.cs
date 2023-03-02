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

			BindDecorators();

			Container.BindInterfacesTo<InputService>().AsSingle();
			Container.BindInterfacesTo<UniTaskRunnerService>().FromNewComponentOnNewGameObject().AsSingle();

			Container.BindInterfacesTo<AssetsService>().AsSingle();
			Container.BindInterfacesTo<SceneTransferService>().AsSingle();

			Container.Bind<GameUiMediator>().AsSingle();

			StartGame();
		}

		private void BindDecorators()
		{
			var timeService = new AccelerateableTimeServiceDecorator(new TimeService());

			Container.BindInterfacesAndSelfTo<AccelerateableTimeServiceDecorator>()
			         .FromInstance(timeService)
			         .AsSingle();
		}

		private void StartGame() => Container.BindInterfacesTo<ToBootstrapOnInitialize>().AsSingle();

		private void BindPrefabs()
		{
			Container.BindInstance<IBalanceTable>(_balanceTable).AsSingle();
			Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtainPrefab).AsSingle();
			Container.BindInterfacesAndSelfTo<User>().FromInstance(_user).AsSingle();
			Container.BindInstance<IResourcesService>(_resources).AsSingle();
		}
	}
}