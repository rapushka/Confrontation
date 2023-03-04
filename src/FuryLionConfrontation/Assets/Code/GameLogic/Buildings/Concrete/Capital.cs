using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public class Capital : Settlement
	{
		public int OwnerPlayerId => Field.Regions[Coordinates].OwnerPlayerId;

		private Barrack _barrack;
		private GoldenMine _goldenMine;

		private readonly List<Generator> _stashedGenerators = new();

		public override float CoolDownDuration => _stashedGenerators.Min((g) => g.CoolDownDuration);

		public void SetStashedBuildings(params Generator[] generators)
		{
			generators.ForEach((g) => g.Invisibility.MakeInvisible());

			_stashedGenerators.Clear();
			_stashedGenerators.Add(this);
			_stashedGenerators.AddRange(generators);
		}

		public override void Action() => _stashedGenerators.ForEach((a) => a.Action());

		public override void LevelUp() => _stashedGenerators.ForEach((a) => a.LevelUp());
	}
}