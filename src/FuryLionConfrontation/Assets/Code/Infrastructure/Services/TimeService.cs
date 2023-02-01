using UnityEngine;

namespace Confrontation
{
	public interface ITimeService
	{
		float FixedDeltaTime { get; }
		float DeltaTime      { get; }
	}

	public class TimeService : ITimeService
	{
		public float FixedDeltaTime => Time.fixedDeltaTime;

		public float DeltaTime => Time.deltaTime;
	}
}