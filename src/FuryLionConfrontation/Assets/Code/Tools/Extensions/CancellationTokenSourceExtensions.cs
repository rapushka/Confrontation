using System.Threading;

namespace Confrontation
{
	public static class CancellationTokenSourceExtensions
	{
		public static CancellationTokenSource CancelAndReplace(this CancellationTokenSource @this)
		{
			@this.Cancel();
			@this.Dispose();
			return new CancellationTokenSource();
		}
	}
}