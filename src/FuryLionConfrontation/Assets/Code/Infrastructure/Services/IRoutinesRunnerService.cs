using System.Collections;
using UnityEngine;

namespace Confrontation
{
	public interface IRoutinesRunnerService
	{
		Coroutine StartRoutine(string methodName, IEnumerator routine);

		void StopRoutine(string methodName);

		void StopAllRoutines();
	}
}