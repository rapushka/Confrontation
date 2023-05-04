using UnityEngine;

namespace Confrontation
{
	public class TimeService : ITimeService
	{
		public float RealFixedDeltaTime => Time.fixedDeltaTime;

		public float RealDeltaTime => Time.deltaTime;

		public float FixedDeltaTime => Time.fixedDeltaTime;

		public float DeltaTime => Time.deltaTime;
	}
}