using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace Confrontation.Editor
{
	// ReSharper disable Unity.PerformanceCriticalCodeInvocation - we don't care about performance in Editor
	public class LevelEditor
	{
		[CanBeNull] private Field _field;

		public void GenerateField(int height, int width)
		{
			DestroyOldField();
			GenerateNewField();
		}

		private static void DestroyOldField()
		{
			var root = Object.FindObjectOfType<CellsRoot>();
			if (root == true)
			{
				Object.DestroyImmediate(root.gameObject);
			}
		}

		private void GenerateNewField() { }

		public string Serialize()
		{
			if (_field is null)
			{
				return $"{nameof(_field).Format()} is null";
			}

			var level = ScriptableObject.CreateInstance<Level>();
			var serializableLevel = new
			{
				level.Sizes,
				VillagesPositions = level.Regions
			};
			return JsonConvert.SerializeObject(serializableLevel, Formatting.Indented);
		}

		public void ToVillage(GameObject gameObject) { }

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