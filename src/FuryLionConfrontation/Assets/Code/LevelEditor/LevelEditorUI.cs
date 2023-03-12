using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class LevelEditorUI : MonoBehaviour, IInitializable
	{
		[Inject] private readonly List<LevelScriptableObject> _levels;

		[SerializeField] private Transform _levelListRoot;

		public Transform LevelListRoot => _levelListRoot;

		public void Initialize() { }
	}
}