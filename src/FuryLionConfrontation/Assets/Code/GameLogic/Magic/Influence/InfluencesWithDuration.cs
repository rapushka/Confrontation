using System.Collections.Generic;
using Zenject;

namespace Confrontation
{
	public class InfluencesWithDuration : InfluencerBase, ILateTickable
	{
		[Inject] private readonly ITimeService _time;

		private readonly List<TimedInfluence> _timedInfluences = new();

		protected override IEnumerable<TargetedInfluence> Influences => _timedInfluences.AsTargetedInfluences();

		public void LateTick()
		{
			foreach (var timedInfluence in _timedInfluences)
			{
				timedInfluence.TimeToLife -= _time.DeltaTime;

				if (timedInfluence.TimeToLife <= 0)
				{
					Remove(timedInfluence);
				}
			}

			_timedInfluences.RemoveIf((ti) => ti.TimeToLife <= 0);
		}

		protected override void AddInfluences(ISpell spell)
		{
			base.AddInfluences(spell);

			_timedInfluences.AddRange(spell.AsTimedInfluences());
		}
	}
}