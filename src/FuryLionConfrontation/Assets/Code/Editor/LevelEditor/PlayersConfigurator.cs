using System;
using System.Collections.Generic;
using System.Linq;
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

	public static class ListExtensions
	{
		public static void Resize<T>(this List<T> list, int newLength)
			where T : new()
		{
			Resize(list, newLength, new T());
		}

		public static void Resize<T>(this List<T> @this, int newLength, T template)
		{
			var oldLength = @this.Count;
			if (newLength < oldLength)
			{
				@this.RemoveRange(newLength, oldLength - newLength);
			}
			else if (newLength > oldLength)
			{
				@this.Capacity = newLength;
				@this.AddRange(Enumerable.Repeat(template, newLength - oldLength));
			}
		}
	}
}