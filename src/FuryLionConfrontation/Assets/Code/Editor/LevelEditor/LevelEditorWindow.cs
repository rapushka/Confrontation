using UnityEditor;

namespace Confrontation.Editor
{
	public class LevelEditorWindow : EditorWindow
	{
		[MenuItem("Tools/Confrontation/LevelEditor")]
		private static void ShowWindow()
		{
			var window = GetWindow<LevelEditorWindow>();
			window.titleContent = new UnityEngine.GUIContent(nameof(LevelEditorWindow));
			window.Show();
		}

		private void OnGUI()
		{
			
		}
	}
}