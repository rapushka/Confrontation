using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Confrontation
{
	public static class CancellationTokenExtensions
	{
		public static async Task<bool> WaitForFixedUpdate(this CancellationToken @this)
			=> await UniTask.WaitForFixedUpdate(cancellationToken: @this).SuppressCancellationThrow();
	}
}