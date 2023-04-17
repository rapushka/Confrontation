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
		}
	}
}