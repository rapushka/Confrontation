using System;
using System.Threading;

namespace Confrontation
{
	public interface IRoutinesRunnerService
	{
		void StopAllRoutines();

		void StartRoutine(Action<CancellationTokenSource> cancelableTask);
	}
}