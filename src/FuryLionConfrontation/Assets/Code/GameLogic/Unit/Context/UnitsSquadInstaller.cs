using UnityEngine;

namespace Confrontation
{
	public class UnitsSquadInstaller : GarrisonInstaller
	{
		[SerializeField] private UnitsSquad _unitsSquad;
		[SerializeField] private UnitMovement _unitMovement;
		[SerializeField] private UnitOrderPerformer _unitOrderPerformer;
		[SerializeField] private Transform _transform;

		public override void InstallBindings()
		{
			base.InstallBindings();

			Container.BindInstance(_unitsSquad).AsSingle();
			Container.BindInstance(_unitMovement).AsSingle();
			Container.BindInstance(_unitOrderPerformer).AsSingle();
			Container.BindInstance(_transform).AsSingle();

			Container.Bind<UnitFighter>().AsSingle();

			BindFactories();
			
			BindStatsDecorators();
		}

		private void BindFactories()
		{
			Container.BindFactory<Cell, IDefenceStrategy, DefenceStrategyFactory>()
			         .FromFactory<DefenceStrategyForCellFactory>();

			Container.BindFactory<Garrison, SingleForceDefenceStrategy, SingleForceDefenceStrategy.Factory>();
			Container.BindFactory<UnitsSquad, Garrison, Cell, BothForcesDefenceStrategy, 
				BothForcesDefenceStrategy.Factory>();
		}

		private void BindStatsDecorators()
		{
			Container.BindSelf<BlizzardInfluenceDecorator>().AsSingle();

			Container.DecorateFromResolve<IUnitStats, BuildingsInfluenceDecorator, BlizzardInfluenceDecorator>();
			Container.Bind<IUnitStats>().To<BlizzardInfluenceDecorator>().FromResolve();
		}
	}
}