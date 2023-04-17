using UnityEngine;

namespace Confrontation
{
	public class UnitsSquadInstaller : GarrisonInstaller
	{
		[SerializeField] private UnitMovement _unitMovement;
		[SerializeField] private UnitOrderPerformer _unitOrderPerformer;

		public override void InstallBindings()
		{
			base.InstallBindings();
			
			Container.BindInstance(_unitMovement).AsSingle();
			Container.BindInstance(_unitOrderPerformer).AsSingle();
		}
	}
}