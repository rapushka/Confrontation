using UnityEngine;

namespace Confrontation
{
	public class Invisibility : MonoBehaviour
	{
		[SerializeField] private Renderer _renderer;

		public void MakeInvisible() => _renderer.enabled = false;
	}
}