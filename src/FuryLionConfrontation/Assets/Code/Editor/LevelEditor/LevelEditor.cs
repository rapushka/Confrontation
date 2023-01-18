using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation.Editor
{
	// ReSharper disable Unity.PerformanceCriticalCodeInvocation - we don't care about performance in Editor
	public class LevelEditor
	{
		[CanBeNull] private Field _field;

		private static Cell CellPrefab => Resources.Load<Cell>("Prefabs/Cell");

		public void GenerateField(int height, int width)
		{
			DestroyOldField();
			GenerateNewField(height, width);
		}

		private static void DestroyOldField()
		{
			var root = Object.FindObjectOfType<CellsRoot>();
			if (root == true)
			{
				Object.DestroyImmediate(root);
			}
		}

		private void GenerateNewField(int height, int width)
		{
			_field = new Field(CellPrefab, height, width);
			_field.Root.gameObject.AddComponent<CellsRoot>();
			_field.GenerateField();
		}
	}
}