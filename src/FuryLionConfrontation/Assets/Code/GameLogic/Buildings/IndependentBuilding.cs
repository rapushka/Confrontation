using Zenject;

namespace Confrontation
{
	public abstract class IndependentBuilding : Building
	{
		[Inject] private readonly Region.Factory _regionsFactory;

		private Region _ownRegion;

		private Region RelatedRegion => Field.Regions[Coordinates];

		private void Start()
		{
			var oldRegionOwnerId = RelatedRegion.OwnerPlayerId;
			_ownRegion = _regionsFactory.Create(RelatedRegion.Id);
			_ownRegion.Coordinates = Coordinates;
			_ownRegion.OwnerPlayerId = oldRegionOwnerId;
			Field.Regions.Add(_ownRegion);
		}
	}
}