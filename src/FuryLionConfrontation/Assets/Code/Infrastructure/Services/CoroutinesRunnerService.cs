using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	public class CoroutinesRunnerService : MonoBehaviour, IRoutinesRunnerService
	{
		private readonly List<IEnumerator> _coroutines = new();

		private void Awake() => DontDestroyOnLoad(gameObject);

		public void StartRoutine(IEnumerator routine)
		{
			_coroutines.Add(routine);
			StartCoroutine(RunAndRemove(routine));
		}

		public void StopAllRoutines()
		{
			_coroutines.ForEach(StopCoroutine);
			_coroutines.Clear();
		}

		private IEnumerator RunAndRemove(IEnumerator routine)
		{
			yield return StartCoroutine(routine);
			_coroutines.Remove(routine);
		}
	}
}