using System.Collections;
using UnityEngine;

namespace Confrontation
{
	public interface IRoutinesRunnerService
	{
		Coroutine StartRoutine(string methodName, IEnumerator coroutine);
		Coroutine StartRoutine(IEnumerator coroutine);
		void      StopRoutine(string methodName);
		void      StopRoutine(Coroutine coroutine);
		void      StopAllRoutines();
	}
}