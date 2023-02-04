using System.Collections.Generic;
using System.Linq;

namespace Confrontation.Editor
{
	public static class StringExtensions
	{
		public static string Pretty(this string @this)
			=> @this
			   .ToCharArray()
			   .ToList()
			   .RemoveUnderscore()
			   .FirstToUpper()
			   .SpacesBeforeCapitalLetters()
			   .AsString();

		private static List<char> RemoveUnderscore(this List<char> chars)
		{
			if (chars.First() == '_')
			{
				chars.RemoveAt(index: 0);
			}

			return chars;
		}

		private static List<char> FirstToUpper(this List<char> chars)
		{
			chars[0] = char.ToUpper(chars[0]);
			return chars;
		}

		private static List<char> SpacesBeforeCapitalLetters(this List<char> chars)
		{
			for (var i = 1; i < chars.Count; i++)
			{
				if (char.IsUpper(chars[i]))
				{
					chars.Insert(index: i, item: ' ');
					i++;
				}
			}

			return chars;
		}

		private static string AsString(this List<char> chars) => new(chars.ToArray());
	}
}