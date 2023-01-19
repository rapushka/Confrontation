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

		private static Village VillagePrefab => Resources.Load<Village>("Prefabs/Village");

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

		public string Serialize()
		{
			if (_field is null)
			{
				return $"{nameof(_field).Format()} is null";
			}

			var level = AssemblyLevel();
			return JsonConvert.SerializeObject(level, Formatting.Indented);
		}

		public void ToVillage(GameObject gameObject)
		{
			var cell = gameObject.GetComponent<Cell>();
			if (PreCondition(cell))
			{
				return;
			}

			var village = Object.Instantiate(original: VillagePrefab, parent: cell.transform);
			village.CellsInRegion.Add(cell);

			cell.Building = village;
		}

		private Level AssemblyLevel()
			=> new()
			{
				Cells = _field.GetCells(),
				Players = new Player.Data[] { new() { Name = "Player" } },
			};

		private static bool PreCondition(Cell cell)
		{
			if (cell == false)
			{
				Debug.LogWarning("Selected object isn't cell!");
				return true;
			}

			if (cell.Building == true)
			{
				Debug.LogWarning("Cell is already Village!");
				return true;
			}

			return false;
		}

		public void UpdateField()
		{
			if (_field is null)
			{
				return;
			}

			var defaultMaterial = Resources.Load<Material>("Materials/HexagonsAsset/Default");
			var regionMaterial = Resources.Load<Material>("Materials/HexagonsAsset/Red");

			foreach (var cell in Object.FindObjectsOfType<Cell>())
			{
				cell.GetComponentInChildren<Renderer>().material = defaultMaterial;
			}

			foreach (var village in Object.FindObjectsOfType<Village>())
			{
				foreach (var cell in village.CellsInRegion)
				{
					cell.GetComponentInChildren<Renderer>().material = regionMaterial;
				}
			}
		}
	}
}