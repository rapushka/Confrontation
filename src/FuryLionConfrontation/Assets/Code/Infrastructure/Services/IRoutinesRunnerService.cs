using System;
using System.Threading;
using System.Threading.Tasks;

namespace Confrontation
{
	public interface IRoutinesRunnerService
	{
		void StopAllRoutines();

		Task StartRoutine(Func<CancellationTokenSource, Task> cancelableTask);

		Task StartRoutine(Func<Task> task);
	}
}