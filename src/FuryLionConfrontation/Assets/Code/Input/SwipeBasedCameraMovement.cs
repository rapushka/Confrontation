using System;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class SwipeBasedCameraMovement : IInitializable, IDisposable
	{
		[Inject] private readonly IInputService _inputService;

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

		private void SwipeStart(Vector3 position) { }

		private void OnDragDropped() { }
	}
}