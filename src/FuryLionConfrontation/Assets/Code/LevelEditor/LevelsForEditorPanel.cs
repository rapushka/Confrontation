using UnityEngine;

namespace Confrontation
{
	public class LevelsForEditorPanel : MonoBehaviour
	{
		[field: SerializeField] public Transform LevelListRoot { get; private set; }
	}
}