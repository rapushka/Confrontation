using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace Confrontation.Editor
{
	// ReSharper disable Unity.PerformanceCriticalCodeInvocation - we don't care about performance in Editor
	public class LevelEditor
	{
		[CanBeNull] private Field _field;

		private static ResourcesService ResourcesService
			=> Resources.Load<ResourcesService>("ScriptableObjects/Resources");

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
			var level = ScriptableObject.CreateInstance<Level>();
			level.SetSizes(new Sizes(height, width));

			_field = new Field(ResourcesService, new AssetsService());
			_field.GetRoot().gameObject.AddComponent<CellsRoot>();
			_field.GenerateField();
		}

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

		private bool IsCanBeVillage(GameObject gameObject, out Cell cell)
		{
			cell = gameObject.GetComponent<Cell>();

			if (_field is null)
			{
				Debug.LogWarning("Field must be not null!");
				return false;
			}

			if (cell == false)
			{
				Debug.LogWarning("Selected object isn't cell!");
				return false;
			}

			if (cell.IsEmpty == false)
			{
				Debug.LogWarning("Cell is already taken!");
				return false;
			}

			return true;
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