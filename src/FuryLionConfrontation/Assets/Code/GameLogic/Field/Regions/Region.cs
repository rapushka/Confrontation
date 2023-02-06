using Zenject;

namespace Confrontation
{
	public class Region : ICoordinated
	{
		[Inject] private readonly IField _field;

		private Coordinates _coordinates;

		public int OwnerPlayerId { get; set; }

		public Coordinates Coordinates
		{
			get => _coordinates;
			set
			{
				_coordinates = value;
				_field.Regions.Add(this);
			}
		}

		public class Factory : PlaceholderFactory<Region>
		{
			public Region Create(RegionData data)
			{
				var region = base.Create();
				region.OwnerPlayerId = data.OwnerPlayerId;
				region.Coordinates = data.VillageCoordinates;
				return region;
			}
		}
	}
}