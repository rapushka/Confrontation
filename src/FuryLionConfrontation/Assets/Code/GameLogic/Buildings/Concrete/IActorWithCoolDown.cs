namespace Confrontation
{
	public interface IActorWithCoolDown
	{
		float CoolDownDuration { get; }

		float PassedDuration { get; set; }

		void Action();
	}
}