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
			
			var newLength = EditorGUI.IntField(rect, _state.Players.Count);
			_state.Players.Resize(newLength);
		}

		private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
		{
			var player = _state.Players[index];
			player.Id = index + 1;
			rect.height = EditorGUIUtility.singleLineHeight;

			EditorGUI.LabelField(rect, "Player");

			rect.x += 50;
			EditorGUI.LabelField(rect, player.Id.ToString());

			rect.x += 50;
			rect.width = 150;

			player.Capital = player.Capital.AsObjectField(rect);
		}

		[Serializable]
		public class State
		{
			public List<Player> Players;
		}
	}
}