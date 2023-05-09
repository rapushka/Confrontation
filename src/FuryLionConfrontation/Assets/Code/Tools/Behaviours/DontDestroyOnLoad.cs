using UnityEngine;

namespace Confrontation
{
	public class DontDestroyOnLoad : MonoBehaviour
	{
		private void Awake() => DontDestroyOnLoad(this);
	}
}