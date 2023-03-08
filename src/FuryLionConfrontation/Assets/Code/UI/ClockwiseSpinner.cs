using UnityEngine;

namespace Confrontation
{
	public class ClockwiseSpinner : MonoBehaviour
	{
		[SerializeField] private float _rotationSpeed;

		private float _rotation;

		private float Rotation
		{
			get => _rotation;
			set
			{
				_rotation = value;
				transform.rotation = Quaternion.Euler(0, 0, value);
			}
		}

		private void OnDisable() => ResetRotation();

		private void Update()
		{
			Rotation -= _rotationSpeed;

			if (Rotation % 360 == 0)
			{
				ResetRotation();
			}
		}

		private void ResetRotation() => Rotation = 0f;
	}
}