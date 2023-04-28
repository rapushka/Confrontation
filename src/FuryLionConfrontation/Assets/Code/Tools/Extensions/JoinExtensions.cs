using System.Collections.Generic;

namespace Confrontation
{
	public static class JoinExtensions
	{
		public static string Join<T>(this IEnumerable<T> @this, string separator)
			=> string.Join(separator, @this);
		
		public static string Join<T>(this IEnumerable<T> @this, char separator)
			=> string.Join(separator, @this);
	}
}