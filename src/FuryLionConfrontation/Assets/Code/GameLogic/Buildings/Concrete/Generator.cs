namespace Confrontation
{
	public abstract class Generator : Building, IActorWithCoolDown
	{
		public abstract float CoolDownDuration { get; }

		public abstract float PassedDuration { get; set; }

		public abstract void Action();
	}
}