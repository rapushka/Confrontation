using UnityEditor;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class LevelEditor : IGuiRenderable
	{
		[Inject] private readonly FieldGenerator _fieldGenerator;
		[Inject] private readonly LevelEditorAssetsService _assets;

		public void GuiRender()
		{
			GUILayout.Button(nameof(Generate).Format()).OnClick(Generate);
		}

		private void Generate()
		{
			_assets.CleanRoot();
			_fieldGenerator.Initialize();
			SaveAll();
		}
		
		private static void SaveAll()
		{
			foreach (var monoBehaviour in Object.FindObjectsOfType<MonoBehaviour>())
			{
				EditorUtility.SetDirty(monoBehaviour);
			}
		}
	}
}