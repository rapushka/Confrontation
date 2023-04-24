namespace Confrontation
{
	public class TimedInfluence : TargetedInfluence
	{
		private float _timeToLife;

		public TimedInfluence(ISpell spell, TargetedInfluence influence)
		{
			Influence = influence.Influence;
			Target = influence.Target;
			TimeToLife = spell.SpellType is SpellType.Active ? 0 : spell.Duration;
			IsPermanent = spell.SpellType is SpellType.Permanent;
		}

		private bool IsPermanent { get; }

		private float TimeToLife
		{
			get => IsPermanent ? float.PositiveInfinity : _timeToLife;
			set => _timeToLife = value;
		}
		
		public bool IsOver => TimeToLife <= 0;

		public void SubtractTimeToLife(float value) => TimeToLife -= IsPermanent ? 0 : value;
	}
}