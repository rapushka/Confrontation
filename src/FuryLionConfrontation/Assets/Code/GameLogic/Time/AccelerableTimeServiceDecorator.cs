using Zenject;

namespace Confrontation.GameLogic
{
	public class AccelerableTimeServiceDecorator : ITimeService, IInitializable
	{
		[Inject] private readonly ITimeService _decoratee;
		[Inject] private readonly IBalanceTable _balanceTable;

		public void Initialize() => Decelerate();

		private float AccelerationCoefficient { get; set; }

		public float RealFixedDeltaTime => _decoratee.RealFixedDeltaTime;

		public float RealDeltaTime => _decoratee.RealDeltaTime;

		public float FixedDeltaTime => _decoratee.FixedDeltaTime * AccelerationCoefficient;

		public float DeltaTime => _decoratee.DeltaTime * AccelerationCoefficient;

		public void Accelerate() => AccelerationCoefficient = _balanceTable.TimeStats.AcceleratedTimeScale;

		public void Decelerate() => AccelerationCoefficient = _balanceTable.TimeStats.NormalTimeScale;
	}
}