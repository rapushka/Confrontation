using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class ActiveInfluences : ITickable
	{
		[Inject] private readonly ITimeService _time;

		private readonly List<TimedInfluence> _influences = new();

		public void Tick()
		{
			foreach (var timedInfluence in _influences)
			{
				timedInfluence.TimeToLife -= _time.DeltaTime;
			}

			_influences.RemoveIf((ti) => ti.TimeToLife <= 0);
		}

		public float Influence(float on, InfluenceTarget withTarget)
			=> _influences
			   .Where((ti) => ti.Target == withTarget)
			   .Aggregate(on, (v, ti) => ti.Influence.Apply(v));

		public void CastSpell(ISpell spell) => _influences.AddRange(spell.AsTimedInfluences());

		public IEnumerable<TimedInfluence> WithTarget(InfluenceTarget target)
			=> _influences.Where((ti) => ti.Target == target);
	}
}