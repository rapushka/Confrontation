using UnityEngine.Assertions;

namespace Confrontation
{
	public class LevelEditorTabsSystem : TabsSystem
	{
		private void OnValidate()
		{
			foreach (var tab in Tabs)
			{
				Assert.IsTrue(tab.Page is LevelEditorPage, $"{tab.Page.GetType().Name} must be LevelEditorPage");
			}
		}

		public LevelEditorPage CurrentPage => (LevelEditorPage)CurrentTab!.Page;
	}
}