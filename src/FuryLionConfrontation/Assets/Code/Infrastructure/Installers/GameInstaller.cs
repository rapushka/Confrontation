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
		[SerializeField] private AudioSource _musicSourcePrefab;
		[SerializeField] private SoundsBehaviourService _soundService;

		public override void InstallBindings()
		{
			BindPrefabs();
			DecorateStatsTable();
			DecorateTimeService();
			BindServices();
			BindTransfers();
			BindProgression();
			BindAudio();

			StartGame();
		}

		private void BindPrefabs()
		{
			Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtainPrefab).AsSingle();
			Container.BindInterfacesAndSelfTo<User>().FromInstance(_user).AsSingle();
			Container.BindInstance<IResourcesService>(_resources).AsSingle();
		}

		private void DecorateStatsTable()
		{
			Container.BindSelf<StatsTable>().FromInstance(_statsTable).AsSingle();
			Container.Bind<IStatsTable>().To<StatsTable>().FromResolve();
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

		private void BindServices()
		{
			Container.BindInterfacesTo<InputService>().AsSingle();
			Container.BindInterfacesTo<UniTaskRunnerService>().FromNewComponentOnNewGameObject().AsSingle();
			Container.BindInterfacesTo<AssetsService>().AsSingle();
			Container.Bind<GameUiMediator>().AsSingle();
		}

		private void BindTransfers()
		{
			Container.BindInterfacesTo<SceneTransferService>().AsSingle();

			Container.Bind<ToMainMenu>().AsSingle();
			Container.Bind<ToGameplay>().AsSingle();
			Container.Bind<ToLevelEditor>().AsSingle();
		}

		private void BindProgression()
		{
			Container.Bind<Progression>().AsSingle();
			Container.Bind<ProgressionManipulator>().AsSingle();
			Container.Bind<IProgressionStorageService>().To<PlayerPrefsProgressionService>().AsSingle();
		}

		private void BindAudio()
		{
			Container.Bind<ISoundService>().FromInstance(_soundService).AsSingle();
			Container.InstantiatePrefab(_musicSourcePrefab);
		}

		private void StartGame() => Container.BindInterfacesTo<ToBootstrapOnInitialize>().AsSingle();
	}
}