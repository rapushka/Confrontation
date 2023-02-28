namespace Confrontation.GameLogic
{
	public class AccelerateableTimeServiceDecorator : ITimeService
	{
		private readonly ITimeService _decoratee;

		public AccelerateableTimeServiceDecorator(ITimeService decoratee) => _decoratee = decoratee;
		
		public float AccelerationCoefficient { get; set; }

		public float FixedDeltaTime => _decoratee.FixedDeltaTime * AccelerationCoefficient;

		public float DeltaTime => _decoratee.DeltaTime * AccelerationCoefficient;
	}
}