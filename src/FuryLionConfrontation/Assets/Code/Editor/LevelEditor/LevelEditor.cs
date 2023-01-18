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
			if (_field is not null)
			{
				Object.DestroyImmediate(_field.Root.gameObject);
			}

			_field = new Field(CellPrefab, height, width);
			_field.GenerateField();
		}
	}
}