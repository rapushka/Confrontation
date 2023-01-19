using JetBrains.Annotations;
using Newtonsoft.Json;
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
				Object.DestroyImmediate(root.gameObject);
			}
		}

		private void GenerateNewField(int height, int width)
		{
			_field = new Field(CellPrefab, height, width);
			_field.GetRoot().gameObject.AddComponent<CellsRoot>();
			_field.GenerateField();
		}

		public void Serialize()
		{
			if (_field is null)
			{
				return;
			}

			var level = new Level
			{
				Cells = _field.GetCells(),
				Players = new Player.Data[] { new() { Name = "Player" } },
			};
			var json = JsonConvert.SerializeObject(level, Formatting.Indented);
			Debug.Log(json);
		}
	}
}