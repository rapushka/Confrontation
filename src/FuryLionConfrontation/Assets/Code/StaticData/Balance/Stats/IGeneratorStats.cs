namespace Confrontation
{
	public interface IGeneratorStats : IStats
	{
		float CoolDown { get; }

		int Amount { get; }
	}
}