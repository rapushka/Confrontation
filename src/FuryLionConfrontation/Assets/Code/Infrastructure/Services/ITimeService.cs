namespace Confrontation
{
	public interface ITimeService
	{
		float RealFixedDeltaTime { get; }
		float RealDeltaTime      { get; }
		float FixedDeltaTime     { get; }
		float DeltaTime          { get; }
	}
}