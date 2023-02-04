using UnityEditor;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class BuildingsCreator : IGuiRenderable
	{
		public void GuiRender()
		{
			GUILayout.Button(nameof(CreateCapital).Pretty()).OnClick(CreateCapital);
		}

		private void CreateCapital()
		{
			
		}
	}
}