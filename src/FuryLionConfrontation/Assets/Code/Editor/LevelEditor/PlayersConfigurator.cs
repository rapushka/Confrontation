using System;
using System.Collections.Generic;
using UnityEditor;
using Zenject;

namespace Confrontation.Editor
{
	public class PlayersConfigurator : IInitializable, IGuiRenderable
	{
		[Inject] private readonly State _state;

		public void Initialize()
		{
			_state.Players ??= new List<Player>();
		}

		public void GuiRender()
		{
			var playersCount = _state.Players.Count;
			EditorGUILayout.PrefixLabel(nameof(_state.Players.Count));

			playersCount = EditorGUILayout.IntField(playersCount);

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