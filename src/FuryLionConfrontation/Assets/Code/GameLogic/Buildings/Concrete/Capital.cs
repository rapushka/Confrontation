using System.Collections.Generic;

namespace Confrontation
{
	public class Capital : Settlement
	{
		public int OwnerPlayerId => Field.Regions[Coordinates].OwnerPlayerId;

		private readonly List<Generator> _stashedGenerators = new();

		public IEnumerable<IActorWithCoolDown> StashedBuildings => _stashedGenerators;

		public void SetStashedBuildings(params Generator[] generators)
		{
			generators.ForEach((g) => g.Invisibility.MakeInvisible());

			_stashedGenerators.Clear();
			_stashedGenerators.AddRange(generators);
		}

		public override void LevelUp()
		{
			base.LevelUp();

			_stashedGenerators.ForEach((a) => a.LevelUp());
		}
	}
}