using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation.Editor
{
	public class RegionsConfigurator : IInitializable, IGuiRenderable, IDisposable
	{
		[Inject] private readonly State _state;
		[Inject] private readonly BuildingsCreator _buildingsCreator;

		private ReorderableList _list;

		public void Initialize()
		{
			_state.Villages ??= new List<Village>();
			_state.Villages = _state.Villages.Union(Object.FindObjectsOfType<Village>()).ToList();

			_list = new ReorderableList
			(
				_state.Villages,
				typeof(Player),
				draggable: false,
				displayHeader: true,
				displayAddButton: false,
				displayRemoveButton: false
			);

			_buildingsCreator.BuildingAdd += OnBuildingAdd;
			_buildingsCreator.BuildingRemove += OnBuildingRemove;
		}

		public void GuiRender()
		{
			_list.drawHeaderCallback = DrawHeader;
			_list.drawElementCallback = DrawElement;
			_list.elementHeightCallback = SetElementHeight;

			_list.DoLayoutList();
		}

		public void Dispose()
		{
			_buildingsCreator.BuildingAdd -= OnBuildingAdd;
			_buildingsCreator.BuildingRemove -= OnBuildingRemove;
		}

		private void OnBuildingAdd(Building building)
		{
			if (building is Village village)
			{
				_state.Villages.Add(village);
			}
		}

		private void OnBuildingRemove(Building building)
		{
			if (building is Village village)
			{
				_state.Villages.Remove(village);
			}
		}

		private void DrawHeader(Rect rect)
		{
			EditorGUI.LabelField(rect, "Regions");

			rect.x += 50;
			rect.width = 50;

			EditorGUI.LabelField(rect, _state.Villages.Count.ToString());
		}

		private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
		{
			var village = _state.Villages[index];
			village.RelatedCell.RelatedRegion = village;
			rect.height = EditorGUIUtility.singleLineHeight;

			DrawVillagePosition(rect, village);

			DrawPlayerOwner(rect, village);

			DrawCellsHeader(rect, village);

			DrawCellsElements(rect, village);

			if (Selection.gameObjects.WithComponent<Cell>().Contains(village.RelatedCell)
			    || Selection.gameObjects.WithComponent<Building>().Contains(village))
			{
				rect.x += 200;
				EditorGUI.LabelField(rect, "<- Selected");
			}
		}

		private static void DrawVillagePosition(Rect rect, Village village)
		{
			var position = village.RelatedCell.transform.position;
			EditorGUI.LabelField(rect, $"{village.GetType().Name.Pretty()} ({position.x:F2}; {position.z:F2})");
		}

		private static void DrawPlayerOwner(Rect rect, Village village)
		{
			rect.y += EditorGUIUtility.singleLineHeight;
			rect.x += 50;

			EditorGUI.LabelField(rect, "Player Owner: ");
			rect.x += 100;
			rect.width = 50;

			village.OwnerPlayerId = EditorGUI.IntField(rect, village.OwnerPlayerId);
		}

		private static void DrawCellsHeader(Rect rect, Village village)
		{
			rect.y += EditorGUIUtility.singleLineHeight;
			rect.y += EditorGUIUtility.singleLineHeight;
			rect.x += 50;

			EditorGUI.LabelField(rect, $"{nameof(village.CellsInRegion)}: ");

			rect.width = 50;
			rect.x += 100;

			var newLength = EditorGUI.IntField(rect, village.CellsInRegion.Count);
			village.CellsInRegion.Resize(newLength, null);
		}

		private static void DrawCellsElements(Rect rect, Village village)
		{
			rect.x += 50;
			rect.y += EditorGUIUtility.singleLineHeight;
			rect.y += EditorGUIUtility.singleLineHeight;

			for (var i = 0; i < village.CellsInRegion.Count; i++)
			{
				var cell = village.CellsInRegion[i];
				rect.y += EditorGUIUtility.singleLineHeight;
				rect.width = 150;
				village.CellsInRegion[i] = cell.AsObjectField(rect);

				if (cell == true)
				{
					cell.RelatedRegion = village;
				}
			}
		}

		private float SetElementHeight(int index)
		{
			var cells = _state.Villages[index].CellsInRegion;
			return EditorGUIUtility.singleLineHeight * (cells.Count + 3) + 5;
		}

		[Serializable]
		public class State
		{
			public List<Village> Villages;
		}
	}
}