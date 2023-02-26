using Zenject;

namespace Confrontation
{
	public class EnemyInstaller : Installer<EnemyInstaller>
	{
		[Inject] private readonly Player _player;

		public override void InstallBindings()
		{
			Container.BindInstance(_player).AsSingle();
			Container.Bind<Our>().AsSingle();
			Container.Bind<DecisionMaker>().AsSingle();
			Container.Bind<Enemy>().AsSingle();
		}
	}
}