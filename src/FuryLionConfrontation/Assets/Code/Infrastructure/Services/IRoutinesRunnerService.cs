using System;
using System.Collections;

namespace Confrontation
{
	public interface IRoutinesRunnerService
	{
		void StartRoutine(Func<IEnumerator> func);

		void StartUnstoppableRoutine(IEnumerator routine);

		void RestartRoutine(Func<IEnumerator> func);

		void StopRoutine(Func<IEnumerator> func);

		void StopAllRoutines();
	}
}