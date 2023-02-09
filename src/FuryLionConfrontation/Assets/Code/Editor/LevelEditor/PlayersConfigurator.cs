using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class PlayersConfigurator : IInitializable, IGuiRenderable
	{
		[Inject] private readonly State _state;

		private ReorderableList _list;

		public void Initialize()
		{
			_state.Players ??= new List<Player>();
			_list = new ReorderableList(_state.Players, typeof(Player));
		}

		public void GuiRender()
		{
			_list.drawHeaderCallback = DrawHeader;
			_list.drawElementCallback = DrawElement;

			_list.DoLayoutList();
		}

		private void DrawHeader(Rect rect)
		{
			EditorGUI.LabelField(rect, nameof(_state.Players));

			rect.x += 50;
			rect.width = 50;
		}

		private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
		{
			var player = _state.Players[index];
			rect.height = EditorGUIUtility.singleLineHeight;

			EditorGUI.LabelField(rect, "Player");

			rect.x += 50;
			EditorGUI.LabelField(rect, player.Id.ToString());

			rect.x += 50;
			rect.width = 150;
		}

		[Serializable]
		public class State
		{
			public List<Player> Players;
		}
	}
}