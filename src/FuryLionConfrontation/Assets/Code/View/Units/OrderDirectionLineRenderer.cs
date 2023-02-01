using System;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class OrderDirectionLineRenderer : IInitializable, ITickable, IDisposable
	{
		[Inject] private readonly LineRenderer _lineRenderer;
		[Inject] private readonly IInputService _input;

		private bool _dragging;

		public void Initialize()
		{
			_input.DragStart += OnDragStart;
			_input.DragEnd += OnDragEnd;
		}

		public void Dispose()
		{
			_input.DragStart -= OnDragStart;
			_input.DragEnd -= OnDragEnd;
		}

		public void Tick()
		{
			if (_dragging)
			{
				_lineRenderer.SetLastPosition(_input.CursorWorldPosition);
			}
		}

		private void OnDragStart(Vector3 position)
		{
			_lineRenderer.AddPosition(position);
			_lineRenderer.AddPosition(_input.CursorWorldPosition);
			_dragging = true;
		}

		private void OnDragEnd()
		{
			_dragging = false;
			_lineRenderer.ClearPositions();
		}
	}
}