using Zenject;

namespace Confrontation
{
	public abstract class IndependentBuilding : Building
	{
		[Inject] private readonly Region.Factory _regionsFactory;

		private Region _ownRegion;

		public override int OwnerPlayerId { get; set; }

		private void Start()
		{
			_ownRegion = _regionsFactory.Create(Field.Regions[Coordinates].Id);
			_ownRegion.Coordinates = Coordinates;
			_ownRegion.OwnerPlayerId = OwnerPlayerId;
			Field.Regions.Add(_ownRegion);
		}
	}
}