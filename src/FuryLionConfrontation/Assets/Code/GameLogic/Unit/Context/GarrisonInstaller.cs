using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GarrisonInstaller : MonoInstaller<GarrisonInstaller>
	{
		[Inject] private readonly IStatsTable _stats;

		[SerializeField] private Garrison _garrison;
		[SerializeField] private TextMeshPro _quantityOfUnitsInSquadView;
		[SerializeField] protected UnitAnimator _unitAnimator;
		[SerializeField] private Animator _animator;

		public override void InstallBindings()
		{
			Container.BindInstance(_garrison).AsSingle();
			Container.BindInstance(_unitAnimator).AsSingle();
			Container.BindInstance(_animator).AsSingle();
			Container.BindInstance(_quantityOfUnitsInSquadView).AsSingle();

			Container.BindInterfacesAndSelfTo<SquadHealth>().AsSingle();

			Container.Bind<UnitFighter>().AsSingle();

			BindStatsDecorators();

			BindFactories();
		}

		private void BindStatsDecorators()
		{
			Container.BindSelf<UnitStats>().FromInstance(_stats.UnitStats).AsSingle();
			Container.BindSelf<BuildingInfluenceDecorator>().AsSingle();

			Container.DecorateFromResolve<IUnitStats, UnitStats, BuildingInfluenceDecorator>();
			Container.Bind<IUnitStats>().To<BuildingInfluenceDecorator>().FromResolve();
		}

		private void BindFactories()
		{
			Container.BindFactory<Cell, IDefenceStrategy, DefenceStrategyFactory>()
			         .FromFactory<DefenceStrategyForCellFactory>();

			Container.BindFactory<Garrison, SingleForceDefenceStrategy, SingleForceDefenceStrategy.Factory>();
			Container.BindFactory<UnitsSquad, Garrison, Cell, BothForcesDefenceStrategy,
				BothForcesDefenceStrategy.Factory>();
		}
	}
}