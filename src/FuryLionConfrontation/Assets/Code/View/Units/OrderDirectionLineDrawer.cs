using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class OrderDirectionLineDrawer : IInitializable, IDisposable
	{
		[Inject] private readonly LineRenderer _lineRenderer;
		[Inject] private readonly IInputService _input;
		[Inject] private readonly User _user;
		[Inject] private readonly IRoutinesRunnerService _routinesRunner;

		public bool IsGivingOrder => _lineRenderer.IsDrawing();
		
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

		private void OnDragStart(ClickReceiver clickReceiver)
		{
			if (clickReceiver.Cell.HasUnits
			    && IsBelongToUser(clickReceiver.Cell))
			{
				_lineRenderer.AddPosition(clickReceiver.transform.position);
				_lineRenderer.AddPosition(CursorPosition);

				_routinesRunner.RestartRoutine(DrawLine);
			}
		}

		private void OnSwipeEnd() => _lineRenderer.ClearPositions();

		private IEnumerator DrawLine()
		{
			while (_lineRenderer.IsDrawing())
			{
				_lineRenderer.SetLastPosition(CursorPosition);
				yield return null;
			}
		}

		private bool IsBelongToUser(Cell cell) => cell.RelatedRegion!.OwnerPlayerId == _user.Player.Id;
	}
}