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

			BindCommands();

			Container.Bind<Enemy>().AsSingle();
		}

		private void BindCommands()
		{
			Container.BindFactory<Building, Cell, BuildBuildingCommand, BuildBuildingCommand.Factory>();
			Container.BindFactory<UnitsSquad, IPlaceable, DirectUnitsCommand, DirectUnitsCommand.Factory>();
		}
	}
}