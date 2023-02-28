namespace Confrontation.GameLogic
{
	public class AccelerateableTimeServiceDecorator : ITimeService
	{
		private readonly ITimeService _decoratee;

		public AccelerateableTimeServiceDecorator(ITimeService decoratee) => _decoratee = decoratee;

		public float AccelerationCoefficient { get; set; } = 1f;

		public float RealFixedDeltaTime => _decoratee.RealFixedDeltaTime;

		public float FixedDeltaTime => _decoratee.FixedDeltaTime * AccelerationCoefficient;

		public float DeltaTime => _decoratee.DeltaTime * AccelerationCoefficient;
	}
}