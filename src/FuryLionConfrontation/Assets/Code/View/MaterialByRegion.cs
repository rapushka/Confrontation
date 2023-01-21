using UnityEngine;

namespace Confrontation.View
{
	public class MaterialByRegion : MonoBehaviour
	{
		[SerializeField] private Renderer _renderer;
		[SerializeField] private Material _neutralMaterial;
		[SerializeField] private Material _redMaterial;

		public void ChangeMaterialToNeutral() => _renderer.material = _neutralMaterial;

		public void ChangeMaterialToRed() => _renderer.material = _redMaterial;
	}
}