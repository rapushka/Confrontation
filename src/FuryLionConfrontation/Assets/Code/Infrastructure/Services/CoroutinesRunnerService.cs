using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Confrontation
{
	public class CoroutinesRunnerService : MonoBehaviour, IRoutinesRunnerService
	{
		private readonly List<IEnumerator> _coroutines = new();

		private CancellationTokenSource _cancellationToken = new();

		private void Awake() => DontDestroyOnLoad(gameObject);

		public void StartRoutine(IEnumerator routine)
		{
			_coroutines.Add(routine);
			StartCoroutine(RunAndRemove(routine));
		}

		public void StopAllRoutines()
		{
			_cancellationToken.Cancel(throwOnFirstException: true);
			_cancellationToken.Dispose();
			_cancellationToken = new CancellationTokenSource();

			_coroutines.ForEach(StopCoroutine);
			_coroutines.Clear();
		}

		public void StartRoutine(Action<CancellationTokenSource> cancelableTask)
			=> cancelableTask.Invoke(_cancellationToken);

		private IEnumerator RunAndRemove(IEnumerator routine)
		{
			yield return StartCoroutine(routine);
			_coroutines.Remove(routine);
		}
	}
}