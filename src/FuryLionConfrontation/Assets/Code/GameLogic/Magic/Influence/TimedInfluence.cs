namespace Confrontation
{
	public class TimedInfluence
	{
		private float _timeToLife;

		public Influence Influence { get; set; }

		public InfluenceTarget Target { get; set; }

		public bool IsPermanent { get; set; }

		public float TimeToLife
		{
			get => IsPermanent ? float.PositiveInfinity : _timeToLife;
			set => _timeToLife = value;
		}
	}
}