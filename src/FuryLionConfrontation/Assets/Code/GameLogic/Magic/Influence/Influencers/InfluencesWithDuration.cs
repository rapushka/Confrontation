using System.Collections.Generic;
using Zenject;

namespace Confrontation
{
	public class InfluencesWithDuration : InfluencerBase, ILateTickable
	{
		[Inject] private readonly ITimeService _time;

		private readonly List<TimedInfluence> _timedInfluences = new();

		protected override IEnumerable<TargetedInfluence> Influences => _timedInfluences;

		public void LateTick()
		{
			for (var i = 0; i < _timedInfluences.Count; i++)
			{
				var timedInfluence = _timedInfluences[i];
				timedInfluence.SubtractTimeToLife(_time.DeltaTime);

				if (timedInfluence.IsOver)
				{
					Remove(timedInfluence);
					_timedInfluences.Remove(timedInfluence);
					i--;
				}
			}
		}

		protected override void AddInfluences(ISpell spell)
		{
			base.AddInfluences(spell);

			_timedInfluences.AddRange(spell.AsTimedInfluences());
		}
	}
}