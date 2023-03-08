using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Confrontation
{
	public class UniTaskRunnerService : MonoBehaviour, IRoutinesRunnerService
	{
		private CancellationTokenSource _cancellationToken = new();

		private void Awake() => DontDestroyOnLoad(gameObject);

		private void OnDestroy() => StopAllRoutines();

		public void StopAllRoutines() => _cancellationToken = _cancellationToken.CancelAndReplace();

		public async Task StartRoutine(Func<CancellationTokenSource, Task> cancelableTask)
			=> await cancelableTask.Invoke(_cancellationToken);

		public async Task StartRoutine(Func<Task> task) => await task.Invoke();
	}
}