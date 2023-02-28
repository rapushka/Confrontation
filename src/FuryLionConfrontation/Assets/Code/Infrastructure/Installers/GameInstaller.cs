using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private LoadingCurtain _loadingCurtainPrefab;
		[SerializeField] private User _user;
		[SerializeField] private ResourcesService _resources;
		[SerializeField] private List<WindowBase> _windows;
		[SerializeField] private BalanceTable _balanceTable;

		public override void InstallBindings()
		{
			BindPrefabs();

			Container.BindInterfacesTo<InputService>().AsSingle();
			Container.BindInterfacesTo<CoroutinesRunnerService>().FromNewComponentOnNewGameObject().AsSingle();
			
			Container.BindInterfacesTo<AssetsService>().AsSingle();
			Container.BindInterfacesTo<TimeService>().AsSingle();
			Container.BindInterfacesTo<SceneTransferService>().AsSingle();

			Container.Bind<GameUiMediator>().AsSingle();

			StartGame();
		}

		private void StartGame() => Container.BindInterfacesTo<ToBootstrapOnInitialize>().AsSingle();

		private void BindPrefabs()
		{
			Container.BindInstance<IBalanceTable>(_balanceTable).AsSingle();
			Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtainPrefab).AsSingle();
			Container.BindInterfacesAndSelfTo<User>().FromInstance(_user).AsSingle();
			Container.BindInstance<IResourcesService>(_resources).AsSingle();
			Container.BindInstance(new TypedDictionary<WindowBase>(_windows)).AsSingle();
		}
	}
}