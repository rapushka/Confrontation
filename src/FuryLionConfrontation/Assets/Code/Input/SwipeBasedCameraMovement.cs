using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class SwipeBasedCameraMovement : IInitializable, IDisposable
	{
		[Inject] private readonly IInputService _inputService;
		[Inject] private readonly IRoutinesRunnerService _routinesRunner;

		public void Initialize()
		{
			_inputService.SwipeStart += SwipeStart;
			_inputService.SwipeEnd += OnDragDropped;
		}

		public void Dispose()
		{
			_inputService.SwipeStart -= SwipeStart;
			_inputService.SwipeEnd -= OnDragDropped;
		}

		private void SwipeStart(Vector3 position)
		{
			_routinesRunner.StartRoutine(nameof(Swipe), Swipe());
		}

		private void OnDragDropped() { }

		private IEnumerator Swipe()
		{
			for (var i = 0; i < 10; i++)
			{
				Debug.Log($"routine working {i}");
				yield return null;
			}
		}
	}
}