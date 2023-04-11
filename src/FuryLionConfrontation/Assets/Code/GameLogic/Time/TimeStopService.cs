using Zenject;

namespace Confrontation
{
	public class TimeStopService : ITimeService
	{
		[Inject] private readonly ITimeService _decoratee;

		private bool _isTimeStopped;

		public float RealFixedDeltaTime => _decoratee.RealFixedDeltaTime;

		public float RealDeltaTime => _decoratee.RealDeltaTime;

		public float FixedDeltaTime => _isTimeStopped ? 0 : _decoratee.FixedDeltaTime;

		public float DeltaTime => _isTimeStopped ? 0 : _decoratee.DeltaTime;

		public void Stop() => _isTimeStopped = true;

		public void Resume() => _isTimeStopped = false;
	}
}