using UnityEngine;

namespace Confrontation.Editor
{
	public static class SpriteExtensions
	{
		public static void DrawPreview(this Sprite @this)
		{
			EditorGUILayoutTools.AsHorizontalGroup
			(
				() => GUILayout.Label("Preview", GUILayout.MaxHeight(Constants.Editor.MaxIconPreviewHeight)),
				() => GUILayout.Label(@this.texture, GUILayout.MaxHeight(Constants.Editor.MaxIconPreviewHeight))
			);
		}
	}
}