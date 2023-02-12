using System;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class OrderDirectionLineRenderer : IInitializable, ITickable, IDisposable
	{
		[Inject] private readonly LineRenderer _lineRenderer;
		[Inject] private readonly IInputService _input;
		[Inject] private readonly User _user;

		private Vector3 CursorPosition => _input.CursorWorldPosition;

		public void Initialize()
		{
			_input.DragStart += OnDragStart;
			_input.SwipeEnd += OnSwipeEnd;
		}

		public void Dispose()
		{
			_input.DragStart -= OnDragStart;
			_input.SwipeEnd -= OnSwipeEnd;
		}

		public void Tick()
		{
			if (_lineRenderer.IsDrawing())
			{
				_lineRenderer.SetLastPosition(CursorPosition);
			}
		}

		private void OnDragStart(ClickReceiver clickReceiver)
		{
			if (clickReceiver.Cell.HasUnits
			    && IsBelongToUser(clickReceiver.Cell))
			{
				_lineRenderer.AddPosition(clickReceiver.transform.position);
				_lineRenderer.AddPosition(CursorPosition);
			}
		}

		private void OnSwipeEnd() => _lineRenderer.ClearPositions();

		private bool IsBelongToUser(Cell cell) => cell.RelatedRegion!.OwnerPlayerId == _user.Player.Id;
	}
}