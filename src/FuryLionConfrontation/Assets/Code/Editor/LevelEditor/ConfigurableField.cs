using System;
using UnityEditor;
using Zenject;

namespace Confrontation.Editor
{
	public class ConfigurableField : IField, IGuiRenderable
	{
		[Inject] private State _state;

		public CoordinatedMatrix<Cell> Cells { get; private set; }

		public void GuiRender()
		{
			EditorGUILayoutUtils.AsHorizontalGroup(Height);
			EditorGUILayoutUtils.AsHorizontalGroup(Width);

			Cells = new CoordinatedMatrix<Cell>(_state.Sizes);
		}

		private void Height()
		{
			EditorGUILayout.PrefixLabel(nameof(_state.Sizes.Height));
			_state.Sizes.Height = EditorGUILayout.IntField(_state.Sizes.Height);
		}

		private void Width()
		{
			EditorGUILayout.PrefixLabel(nameof(_state.Sizes.Width));
			_state.Sizes.Width = EditorGUILayout.IntField(_state.Sizes.Width);
		}

		[Serializable]
		public class State
		{
			public Sizes Sizes;
		}
	}
}