using UnityEngine;

namespace Confrontation
{
	public class TimeService : ITimeService
	{
		public float FixedDeltaTime => Time.fixedDeltaTime;

		public float DeltaTime => Time.deltaTime;
	}
}