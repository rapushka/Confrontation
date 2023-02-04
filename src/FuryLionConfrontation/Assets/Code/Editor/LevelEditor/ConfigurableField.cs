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
			_state.Sizes.Height = EditorGUILayout.IntField(_state.Sizes.Height);
			_state.Sizes.Width = EditorGUILayout.IntField(_state.Sizes.Width);
			
			Cells = new CoordinatedMatrix<Cell>(_state.Sizes);
		}

		[Serializable]
		public class State
		{
			public Sizes Sizes;
		}
	}
}