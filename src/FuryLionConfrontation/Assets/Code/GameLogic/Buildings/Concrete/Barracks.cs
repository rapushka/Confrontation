using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Barracks : Building, IActorWithCoolDown
	{
		[Inject] private readonly IAssetsService _assets;

		[field: SerializeField] public float CoolDownDuration { get; private set; } = 10f;

		[field: SerializeField] public UnitsSquad UnitPrefab { get; private set; }

		private UnitsSquad _currentSquad;

		public float PassedDuration { get; set; }

		private bool HaveSquad => _currentSquad == true;

		public void Action() => CreateUnit();

		private void CreateUnit()
		{
			if (HaveSquad)
			{
				_currentSquad.QuantityOfUnits++;
			}
			else
			{
				_currentSquad = _assets.Instantiate(UnitPrefab);
				_currentSquad.QuantityOfUnits = 1;
			}
		}
	}
}