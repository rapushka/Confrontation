using System.Linq;
using UnityEngine;
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

		public T GetPageOfType<T>() => Tabs.Select((t) => t.Page).OfType<T>().Single();

		public LevelEditorPage CurrentPage => (LevelEditorPage)CurrentTab!.Page;
	}
}