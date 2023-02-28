namespace Confrontation
{
	public interface ITimeService
	{
		float FixedDeltaTime { get; }
		float DeltaTime      { get; }
	}
}