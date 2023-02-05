using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class LevelEditor : IGuiRenderable
	{
		[Inject] private readonly FieldGenerator _fieldGenerator;
		[Inject] private readonly LevelEditorAssetsService _assets;
		private const int Indent = 25;

		public void GuiRender()
		{
			GUILayout.Space(Indent);
			GUILayout.Button(nameof(GenerateField).Pretty()).OnClick(GenerateField);
			GUILayout.Space(Indent);
		}

		private void GenerateField()
		{
			_assets.CleanRoot();
			_fieldGenerator.Initialize();
		}
	}
}