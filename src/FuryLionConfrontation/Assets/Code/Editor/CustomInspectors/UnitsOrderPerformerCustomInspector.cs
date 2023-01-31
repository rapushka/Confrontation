using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	[CustomEditor(typeof(UnitOrderPerformer))]
	public class UnitsOrderPerformerCustomInspector : UnityEditor.Editor
	{
		private UnitOrderPerformer Target => (UnitOrderPerformer)target;

		// ReSharper disable Unity.PerformanceCriticalCodeInvocation - We don't care about performance in editor
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUILayout.Label("Debug tools");
			EditorGUILayoutUtils.AsHorizontalGroup(TargetCellObjectField);
			GUILayout.Button(nameof(MoveTo).Format()).OnClick(MoveTo);
		}

		private void TargetCellObjectField()
		{
			GUILayout.Label("Target Cell: ");
			Target.UpdateTargetCell(via: (c) => c.AsObjectField());
		}

		private void MoveTo() => Target.MoveTo(Target.GetTargetCell(), Target.GetQuantityOfUnits());
	}
}