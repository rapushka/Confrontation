using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	public class Invisibility : MonoBehaviour
	{
		[SerializeField] private List<Renderer> _renderers;

		public void MakeInvisible() => _renderers.ForEach((r) => r.enabled = false);
	}
}