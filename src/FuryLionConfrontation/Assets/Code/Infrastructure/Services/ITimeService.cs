namespace Confrontation
{
	public interface ITimeService
	{
		float RealFixedDeltaTime { get; }
		float FixedDeltaTime     { get; }
		float DeltaTime          { get; }
	}
}