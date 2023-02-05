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
		[Inject] private readonly PlayersConfigurator _players;
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
			rect.height = EditorGUIUtility.singleLineHeight;

			var position = village.RelatedCell.transform.position;
			EditorGUI.LabelField(rect, $"{village.GetType().Name.Pretty()} ({position.x:F2}; {position.z:F2})");

			rect.x += 150;

			EditorGUI.LabelField(rect, "Player Owner: ");

			rect.y += EditorGUIUtility.singleLineHeight;
			EditorGUI.LabelField(rect, $"{nameof(village.CellsInRegion)}: ");

			rect.width = 50;
			var newLength = EditorGUI.IntField(rect, village.CellsInRegion.Count);
			village.CellsInRegion.Resize(newLength, null);

			for (var i = 0; i < village.CellsInRegion.Count; i++)
			{
				var cell = village.CellsInRegion[i];
				rect.y += EditorGUIUtility.singleLineHeight;
				rect.width = 150;
				village.CellsInRegion[i] = cell.AsObjectField(rect);
			}

			rect.x += 50;
		}

		private float SetElementHeight(int index)
		{
			var village = _state.Villages[index];
			return EditorGUIUtility.singleLineHeight * (village.CellsInRegion.Count + 2) + 5;
		}

		[Serializable]
		public class State
		{
			public List<Village> Villages;
		}
	}
}