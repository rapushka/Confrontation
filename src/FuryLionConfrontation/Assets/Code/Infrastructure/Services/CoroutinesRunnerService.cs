using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	public class CoroutinesRunnerService : MonoBehaviour, IRoutinesRunnerService
	{
		private readonly Dictionary<string, IEnumerator> _coroutines = new();

		private void Awake() => DontDestroyOnLoad(gameObject);

		public Coroutine StartRoutine(string methodName, IEnumerator routine)
		{
			_coroutines.Add(methodName, routine);
			return StartCoroutine(RunAndRemove(methodName, routine));
		}

		public void StopRoutine(string methodName)
		{
			var routine = _coroutines.GetValueOrDefault(methodName);
			if (routine is not null)
			{
				StopCoroutine(routine);
			}

			_coroutines.Remove(methodName);
		}

		public void StopAllRoutines()
		{
			foreach (var pair in _coroutines)
			{
				StopCoroutine(pair.Value);
			}

			_coroutines.Clear();
		}

		private IEnumerator RunAndRemove(string methodName, IEnumerator routine)
		{
			yield return StartCoroutine(routine);
			_coroutines.Remove(methodName);
		}
	}
}