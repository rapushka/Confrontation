using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	public class CoroutinesRunnerService : MonoBehaviour, IRoutinesRunnerService
	{
		private readonly Dictionary<string, IEnumerator> _startedRoutines = new();

		private void Awake() => DontDestroyOnLoad(gameObject);

		public void StartRoutine(string methodName, IEnumerator routine)
		{
			_startedRoutines.Add(methodName, routine);
			StartCoroutine(RunAndRemove(methodName, routine));
		}

		public void StopRoutine(string methodName)
		{
			if (_startedRoutines.TryGetValue(methodName, out var routine))
			{
				StopCoroutine(routine);
				_startedRoutines.Remove(methodName);
			}
		}

		public void StopAllRoutines()
		{
			foreach (var pair in _startedRoutines)
			{
				StopCoroutine(pair.Value);
			}

			_startedRoutines.Clear();
		}

		private IEnumerator RunAndRemove(string methodName, IEnumerator routine)
		{
			yield return StartCoroutine(routine);
			_startedRoutines.Remove(methodName);
		}
	}
}