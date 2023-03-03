using Zenject;

namespace Confrontation.GameLogic
{
	public class AccelerateableTimeServiceDecorator : ITimeService, IInitializable
	{
		[Inject] private readonly IBalanceTable _balanceTable;

		private readonly ITimeService _decoratee;

		public AccelerateableTimeServiceDecorator(ITimeService decoratee) => _decoratee = decoratee;

		public void Initialize() => Decelerate();

		private float AccelerationCoefficient { get; set; }

		public float RealFixedDeltaTime => _decoratee.RealFixedDeltaTime;

		public float FixedDeltaTime => _decoratee.FixedDeltaTime * AccelerationCoefficient;

		public float DeltaTime => _decoratee.DeltaTime * AccelerationCoefficient;

		public void Accelerate() => AccelerationCoefficient = _balanceTable.TimeStats.AcceleratedTimeScale;

		public void Decelerate() => AccelerationCoefficient = _balanceTable.TimeStats.NormalTimeScale;
	}
}