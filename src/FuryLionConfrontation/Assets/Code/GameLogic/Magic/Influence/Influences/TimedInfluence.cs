namespace Confrontation
{
	public class TimedInfluence : TargetedInfluence
	{
		private float _timeToLife;

		public TimedInfluence(ISpell spell, TargetedInfluence influence)
		{
			Influence = influence.Influence;
			Target = influence.Target;
			_timeToLife = spell.Duration;
		}

		public bool IsOver => _timeToLife <= 0;

		public void SubtractTimeToLife(float value) => _timeToLife -= value;
	}
}