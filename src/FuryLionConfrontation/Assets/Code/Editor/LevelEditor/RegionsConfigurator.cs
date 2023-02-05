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
	public class RegionsConfigurator : IInitializable, IGuiRenderable
	{
		[Inject] private readonly State _state;
		[Inject] private readonly PlayersConfigurator _players;

		private ReorderableList _list;

		public void Initialize()
		{
			_state.Villages ??= new List<Village>();
			_list = new ReorderableList
			(
				_state.Villages,
				typeof(Player),
				draggable: false,
				displayHeader: true,
				displayAddButton: false,
				displayRemoveButton: false
			);
		}

		public void GuiRender()
		{
			_list.drawHeaderCallback = DrawHeader;
			_list.drawElementCallback = DrawElement;

			_list.DoLayoutList();
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
			rect.width = 50;
			
			var newLength = EditorGUI.IntField(rect, village.CellsInRegion.Count);
			village.CellsInRegion.Resize(newLength, null);

			rect.x += 50;
		}

		[Serializable]
		public class State
		{
			public List<Village> Villages;
		}
	}
}