using System.Collections;

namespace Confrontation
{
	public interface IRoutinesRunnerService
	{
		void StartRoutine(string methodName, IEnumerator routine);

		void StopRoutine(string methodName);

		void StopAllRoutines();
	}
}