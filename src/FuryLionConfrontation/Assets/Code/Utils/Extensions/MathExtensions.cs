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

		public static float ReduceBy(this int @this, float percent)
			=> @this * (1 - percent);
	}
}