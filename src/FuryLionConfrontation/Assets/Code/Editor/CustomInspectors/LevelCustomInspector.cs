using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	[CustomEditor(typeof(Level))]
	public class LevelCustomInspector : UnityEditor.Editor
	{
		private Level Target => (Level)target;

		// ReSharper disable Unity.PerformanceCriticalCodeInvocation - we don't care about performance in Editor
		public override void OnInspectorGUI()
		{
			GUILayout.Button(nameof(CollectVillages).Format()).OnClick(CollectVillages);

			base.OnInspectorGUI();
		}

		private void CollectVillages()
			=> Target.Value.Villages = FindObjectsOfType<Village>().Select((v) => v.Value).ToArray();
	}
}