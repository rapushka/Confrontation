using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	public class CoroutinesRunnerService : MonoBehaviour, IRoutinesRunnerService
	{
		private readonly Dictionary<string, IEnumerator> _stoppableRoutines = new();

		private readonly List<IEnumerator> _unstoppableRoutines = new();

		private void Awake() => DontDestroyOnLoad(gameObject);

		public void StartRoutine(Func<IEnumerator> func)
		{
			var methodName = func.Method.Name;
			var routine = func.Invoke();

			_stoppableRoutines.Add(methodName, routine);
			StartCoroutine(RunAndRemove(methodName, routine));
		}

		public void RestartRoutine(Func<IEnumerator> func)
		{
			StopRoutine(func);
			StartRoutine(func);
		}

		public void StartUnstoppableRoutine(IEnumerator routine)
		{
			_unstoppableRoutines.Add(routine);
			StartCoroutine(RunAndRemove(routine));
		}

		public void StopRoutine(Func<IEnumerator> func)
		{
			var methodName = func.Method.Name;

			if (_stoppableRoutines.TryGetValue(methodName, out var routine))
			{
				StopCoroutine(routine);
				_stoppableRoutines.Remove(methodName);
			}
		}

		public void StopAllRoutines()
		{
			_stoppableRoutines.ForEach((p) => StopCoroutine(p.Value));
			_unstoppableRoutines.ForEach(StopCoroutine);

			_stoppableRoutines.Clear();
			_unstoppableRoutines.Clear();
		}

		private IEnumerator RunAndRemove(string methodName, IEnumerator routine)
		{
			yield return StartCoroutine(routine);
			_stoppableRoutines.Remove(methodName);
		}

		private IEnumerator RunAndRemove(IEnumerator routine)
		{
			yield return StartCoroutine(routine);
			_unstoppableRoutines.Remove(routine);
		}
	}
}