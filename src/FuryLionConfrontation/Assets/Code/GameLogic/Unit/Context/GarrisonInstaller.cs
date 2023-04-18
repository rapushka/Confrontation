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
			Container.Bind<IUnitStats>().FromInstance(_stats.UnitStats).AsSingle();
			// Stats = new BuildingInfluenceDecorator(baseStats, OwnerPlayerId, Field, this);
		}
	}
}