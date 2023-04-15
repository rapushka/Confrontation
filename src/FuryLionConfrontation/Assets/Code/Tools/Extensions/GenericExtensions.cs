using System;

namespace Confrontation
{
	public static class GenericExtensions
	{
		public static T With<T>(this T @this, Action<T> action)
		{
			action.Invoke(@this);
			return @this;
		}

		public static TOut Cast<TIn, TOut>(this TIn @this)
			where TOut : TIn
			=> (TOut)@this;
	}
}