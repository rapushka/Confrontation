using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	public class RoutinesRunnerService : MonoBehaviour, IRoutinesRunnerService
	{
		private readonly Dictionary<string, Coroutine> _coroutines = new();

		public Coroutine StartRoutine(string methodName, IEnumerator coroutine)
		{
			var startedCoroutine = StartRoutine(coroutine);
			_coroutines.Add(methodName, startedCoroutine);
			return startedCoroutine;
		}

		public Coroutine StartRoutine(IEnumerator coroutine) => StartCoroutine(coroutine);

		public void StopRoutine(string methodName)
		{
			var coroutine = _coroutines[methodName];
			_coroutines.Remove(methodName);
			StopRoutine(coroutine);
		}

		public void StopRoutine(Coroutine coroutine) => StopCoroutine(coroutine);

		public void StopAllRoutines()
		{
			foreach (var pair in _coroutines)
			{
				StopRoutine(pair.Value);
			}

			_coroutines.Clear();
		}
	}
}