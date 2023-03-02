using System.Collections;

namespace Confrontation
{
	public interface IRoutinesRunnerService
	{
		void StartRoutine(IEnumerator routine);

		void StopAllRoutines();
	}
}