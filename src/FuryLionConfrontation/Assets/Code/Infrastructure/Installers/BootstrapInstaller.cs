using UnityEditor.Search;
using UnityEngine;
using Zenject;

namespace Code
{
	public class BootstrapInstaller : MonoInstaller
	{
		[SerializeField] private Cell _cellPrefab;

		// ReSharper disable Unity.PerformanceAnalysis - Method call only on initialization
		public override void InstallBindings()
		{
			const int cellsCount = 20;

			for (var x = 0; x < cellsCount; x++)
			{
				for (var z = 0; z < cellsCount; z++)
				{
					var cell = Instantiate(_cellPrefab);
					cell.Coordinates = new Vector2Int(x, z);

					var position = cell.transform.position;
					position.x = z % 2 == 0 ? x : x + 0.5f;
					position.y = 0;
					position.z = z * (3 / (2 * Mathf.Sqrt(3)));

					cell.transform.position = position;
				}
			}
		}
	}
}