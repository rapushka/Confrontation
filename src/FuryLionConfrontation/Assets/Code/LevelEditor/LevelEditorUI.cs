using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Confrontation
{
	public class LevelEditorUI : MonoBehaviour, IInitializable
	{
		[Inject] private readonly List<LevelScriptableObject> _levels;

		[SerializeField] private LevelListItem levelListItemPrefab;
		[SerializeField] private Transform levelListContent;
		[SerializeField] private InputField newLevelNameInput;

		public void Initialize()
		{
			Debug.Log("heyo");
			foreach (var level in _levels)
			{
				Debug.Log(level.name);
			}
		}
	}
}