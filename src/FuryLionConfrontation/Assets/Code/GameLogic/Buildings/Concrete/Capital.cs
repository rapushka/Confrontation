using System.Linq;

namespace Confrontation
{
	public class Capital : Settlement
	{
		public void SetStashedBuildings(params Building[] buildings)
		{
			buildings.ForEach((b) => b.Invisibility.MakeInvisible());

			buildings.ForEach((b) => b.Coordinates = Coordinates);
			Field.StashedBuildings.AddRange(buildings);
		}

		public override string Name => nameof(Capital);

		public override void LevelUp()
		{
			base.LevelUp();

			Field.StashedBuildings.Where((b) => b.Coordinates == Coordinates).ForEach((a) => a.LevelUp());
		}
	}
}