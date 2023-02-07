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

			rect.height = EditorGUIUtility.singleLineHeight;

			if (village.RelatedCell == true)
			{
				rect.Draw(village);
			}
		}

		private float SetElementHeight(int index)
		{
			var cells = _state.Villages[index].CellsInRegion;
			return EditorGUIUtility.singleLineHeight * (cells.Count() + 3) + 5;
		}

		[Serializable]
		public class State
		{
			public List<Village> Villages;
		}
	}
}