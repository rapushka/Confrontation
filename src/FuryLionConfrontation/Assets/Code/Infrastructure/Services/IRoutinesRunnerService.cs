using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Confrontation
{
	public interface IRoutinesRunnerService
	{
		void StartRoutine(IEnumerator routine);

		void StopAllRoutines();

		void StartRoutine(Action<CancellationTokenSource> cancelableTask);
	}
}