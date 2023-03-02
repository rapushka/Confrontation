using System;

namespace Confrontation
{
	public static class TimeSpanExtensions
	{
		public static TimeSpan FromSeconds(this float @this) => TimeSpan.FromSeconds(@this);

		public static TimeSpan FromMilliseconds(this float @this) => TimeSpan.FromMilliseconds(@this);
	}
}