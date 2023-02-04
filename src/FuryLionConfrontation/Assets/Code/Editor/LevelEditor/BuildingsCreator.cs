using UnityEditor;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class BuildingsCreator : IGuiRenderable
	{
		[Inject] private readonly IAssetsService _assets;
		[Inject] private readonly IResourcesService _resources;

		public void GuiRender()
		{
			GUILayout.Label("Create Building On Selected Cell");
			GUILayout.Button(nameof(CreateCapital).Pretty()).OnClick(CreateCapital);
			GUILayout.Button(nameof(DestroyBuilding).Pretty()).OnClick(DestroyBuilding);
		}

		private void DestroyBuilding()
		{
			
		}

		private void CreateCapital() => Selection.gameObjects.ForEach(CreateCapitalForCell);

		private void CreateCapitalForCell(GameObject gameObject)
		{
			if (gameObject.IsValidForBuilding(out var cell) == false)
			{
				return;
			}

			var capital = _assets.Instantiate(_resources.CapitalPrefab, cell.transform);
			capital.RelatedCell = cell;
			cell.Building = capital;
		}
	}
}