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
			var playersCount = _state.Players.Count;
			EditorGUILayout.PrefixLabel(nameof(_state.Players.Count));

			playersCount = EditorGUILayout.IntField(playersCount);

			UpdateListSize(playersCount);
			_list.drawHeaderCallback = DrawHeader;
			_list.drawElementCallback = DrawElement;

			_list.DoLayoutList();
		}

		// ReSharper disable once MemberCanBeMadeStatic.Local - is use _state field
		private void DrawHeader(Rect rect) => EditorGUI.LabelField(rect, nameof(_state.Players));

		private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
		{
			var player = _state.Players[index];
			player.Id = index + 1;
			rect.height = EditorGUIUtility.singleLineHeight;

			EditorGUI.LabelField(rect, "Player");

			rect.x += 50;
			EditorGUI.LabelField(rect, player.Id.ToString());

			rect.x += 50;

			player.Capital = player.Capital.AsObjectField(rect);
		}

		private void UpdateListSize(int playersCount)
		{
			if (playersCount != _state.Players.Count)
			{
				_state.Players.Resize(playersCount);
			}
		}

		[Serializable]
		public class State
		{
			public List<Player> Players;
		}
	}
}