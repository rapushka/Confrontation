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

		private const float MinDistanceForLine = 1f;

		private bool _isDragging;
		private Vector3 _startReceiver;

		private Vector3 CursorPosition => _input.CursorWorldPosition;

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
			if (Vector3.Distance(_startReceiver, CursorPosition) < MinDistanceForLine)
			{
				_lineRenderer.RemoveLastPosition();
				return;
			}

			if (_isDragging == false)
			{
				return;
			}

			if (_lineRenderer.positionCount > 1)
			{
				_lineRenderer.SetLastPosition(CursorPosition);
			}
			else
			{
				_lineRenderer.AddPosition(CursorPosition);
			}
		}

		private void OnDragEnd()
		{
			_isDragging = false;
			_lineRenderer.ClearPositions();
		}

		private void OnDragStart(ClickReceiver clickReceiver)
		{
			if (clickReceiver.Cell.HasUnits == false
			    && clickReceiver.Cell.RelatedRegion!.OwnerPlayerId == _user.Player.Id)
			{
				return;
			}

			_startReceiver = clickReceiver.transform.position;
			_lineRenderer.AddPosition(_startReceiver);
			_lineRenderer.AddPosition(CursorPosition);
			_isDragging = true;
		}
	}
}