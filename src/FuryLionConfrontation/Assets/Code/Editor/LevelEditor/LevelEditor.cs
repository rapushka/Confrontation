using System;
using NSubstitute;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class LevelEditor : IGuiRenderable
	{
		[Inject] private readonly FieldGenerator _fieldGenerator;
		[Inject] private State _state;
		[Inject] private readonly LevelEditorAssetsService _assets;

		public void GuiRender()
		{
			_state.Sizes.Height = EditorGUILayout.IntField(_state.Sizes.Height);
			_state.Sizes.Width = EditorGUILayout.IntField(_state.Sizes.Width);

			GUILayout.Button(nameof(Generate).Format()).OnClick(Generate);
		}

		private void Generate()
		{
			_assets.CleanRoot();
			_fieldGenerator.GetPrivateField<IField>("_field").Cells.Returns(NewField());
			_fieldGenerator.Initialize();
		}

		private CoordinatedMatrix<Cell> NewField() => new(_state.Sizes);

		[Serializable]
		public class State
		{
			public Sizes Sizes;
		}
	}
}