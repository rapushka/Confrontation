using System;
using System.Threading;
using UnityEngine;

namespace Confrontation
{
	public class UniTaskRunnerService : MonoBehaviour, IRoutinesRunnerService
	{
		private CancellationTokenSource _cancellationToken = new();

		private void Awake() => DontDestroyOnLoad(gameObject);

		private void OnDestroy() => StopAllRoutines();

		public void StopAllRoutines()
		{
			_cancellationToken.Cancel(throwOnFirstException: true);
			_cancellationToken.Dispose();
			_cancellationToken = new CancellationTokenSource();
		}

		public void StartRoutine(Action<CancellationTokenSource> cancelableTask)
			=> cancelableTask.Invoke(_cancellationToken);
	}
}