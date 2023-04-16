using System;

namespace Confrontation.Editor
{
	public static class WindowExtensions
	{
		public static void OnClick(this bool @this, Action @do)
		{
			if (@this)
			{
				@do.Invoke();
			}
		}
	}
}