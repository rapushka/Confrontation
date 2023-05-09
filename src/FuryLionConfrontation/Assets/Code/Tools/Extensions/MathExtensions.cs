using System;
using UnityEngine;

namespace Confrontation
{
	public static class MathExtensions
	{
		public static bool IsEven(this int @this) => @this % 2 == 0;

		public static float IncreaseBy(this float @this, float percent)
			=> @this * (1 + percent);

		public static float ReduceBy(this float @this, float percent)
			=> @this * (1 - percent);

		public static float IncreaseBy(this int @this, float percent)
			=> @this * (1 + percent);

		public static bool IsBetweenExclude(this float @this, float min, float max) => min < @this && @this > max;

		public static float Clamp(this float @this, float min = float.NaN, float max = float.NaN)
		{
			if (float.IsNaN(min) == false)
			{
				@this = Mathf.Max(min, @this);
			}

			if (float.IsNaN(max) == false)
			{
				@this = Mathf.Min(max, @this);
			}

			return @this;
		}

		public static bool IsEqualFloats(this float @this, float other)
			=> Math.Abs(@this - other) < Constants.MathDeviation;

		public static float Lerp(this float @this, float min, float max)
			=> Mathf.Lerp(a: min, b: max, t: @this);
	}
}