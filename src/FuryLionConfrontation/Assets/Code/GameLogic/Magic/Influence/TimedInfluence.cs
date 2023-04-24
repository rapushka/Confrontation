namespace Confrontation
{
	public class TimedInfluence : TargetedInfluence
	{
		private float _timeToLife;

		public bool IsPermanent { get; set; }

		public float TimeToLife
		{
			get => IsPermanent ? float.PositiveInfinity : _timeToLife;
			set => _timeToLife = value;
		}
	}
}