using System;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

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
			throw new NotImplementedException();
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