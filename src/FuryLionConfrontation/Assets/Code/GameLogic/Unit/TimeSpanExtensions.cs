using System;

namespace Confrontation
{
	public static class TimeSpanExtensions
	{
		public static TimeSpan FromSeconds(this float @this) => TimeSpan.FromSeconds(@this);
	}
}