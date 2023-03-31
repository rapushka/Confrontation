using UnityEditor;
using Zenject;

namespace Confrontation
{
	public class LevelSaver : ILevelSaver
	{
		[Inject] private readonly ILevelSelector _levelSelector;

		public void Save(ILevel level)
		{
			var levelScriptableObject = (LevelScriptableObject)_levelSelector.SelectedLevel;
			levelScriptableObject.Sizes = level.Sizes;
			levelScriptableObject.Buildings = level.Buildings;
			levelScriptableObject.Regions = level.Regions;

#if UNITY_EDITOR
			EditorUtility.SetDirty(levelScriptableObject);
			AssetDatabase.SaveAssets();
#endif
		}
	}
}