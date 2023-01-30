using UnityEngine;

namespace Confrontation
{
	public class LookAtCamera : MonoBehaviour
	{
		private Camera _camera;

		private Vector3 TargetForward => transform.position - _camera.transform.position;

		private void Start() => _camera = Camera.main;

		private void LateUpdate()
		{
			transform.rotation = Quaternion.LookRotation(TargetForward);
			transform.eulerAngles = transform.eulerAngles.SetY(0f);
		}
	}
}