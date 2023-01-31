using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	[CustomEditor(typeof(UnitsSquad))]
	public class UnitsSquadCustomInspector : UnityEditor.Editor
	{
		private Cell _targetCell;

		private UnitsSquad Target => (UnitsSquad)target;

		// ReSharper disable Unity.PerformanceCriticalCodeInvocation - We don't care about performance in editor
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUILayout.Label("Debug tools");
			EditorGUILayoutUtils.AsHorizontalGroup(TargetCellObjectField);
			GUILayout.Button(nameof(MoveToTargetCell).Format()).OnClick(MoveToTargetCell);
		}

		private void TargetCellObjectField()
		{
			GUILayout.Label("Target Cell: ");
			_targetCell = _targetCell.AsObjectField();
		}

		private void MoveToTargetCell() => Target.MoveTo(_targetCell, Target.QuantityOfUnits);
	}
}