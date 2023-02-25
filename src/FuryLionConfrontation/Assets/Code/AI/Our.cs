using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public class Our
	{
		private readonly IField _field;
		private readonly int _id;

		public Our(IField field, int id)
		{
			_field = field;
			_id = id;
		}

		public UnitsSquad[] Units => _field.LocatedUnits.Where(IsOurUnit).AsArray();

		private bool IsOurUnit(UnitsSquad unit) => unit is not null && unit.OwnerPlayerId == _id;

		public IEnumerable<Village> NeighboursFor(UnitsSquad randomSquad)
			=> _field.Buildings
			         .OfType<Village>()
			         .Where((v) => IsOnNeighbourRegions(randomSquad, v));

		private bool IsOnNeighbourRegions(UnitsSquad squad, Building village)
			=> IsNeighbours(squad.LocationCell.RelatedRegion, village.RelatedCell.RelatedRegion);

		private bool IsNeighbours(Region currentRegion, Region targetRegion)
			=> _field.Neighboring.IsNeighbours(targetRegion, currentRegion)
			   && targetRegion != currentRegion;
	}
}