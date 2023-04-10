using Zenject;

namespace Confrontation
{
	public abstract class IndependentBuilding : Building
	{
		[Inject] private readonly IField _iField;
		[Inject] private readonly Region.Factory _regionsFactory;

		private Region _ownRegion;

		public int PlayerOwnerId { get; set; }

		private void Start()
		{
			_ownRegion = _regionsFactory.Create(_iField.Regions[Coordinates].Id);
			_ownRegion.Coordinates = Coordinates;
			_ownRegion.OwnerPlayerId = PlayerOwnerId;
			_iField.Regions.Add(_ownRegion);
		}
	}
}