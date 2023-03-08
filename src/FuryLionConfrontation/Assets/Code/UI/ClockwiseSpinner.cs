using System.Threading;
using UnityEngine;

namespace Confrontation
{
	public class ClockwiseSpinner : MonoBehaviour
	{
		[SerializeField] private float _rotationSpeed;

		private float _rotation;
		private CancellationTokenSource _tokenSource = new();

		private float Rotation
		{
			get => _rotation;
			set
			{
				_rotation = value;
				transform.rotation = Quaternion.Euler(0, 0, value);
			}
		}

		private void OnEnable() => Rotate();

		private void OnDisable()
		{
			_tokenSource = _tokenSource.CancelAndReplace();
			ResetRotation();
		}

		private async void Rotate()
		{
			while (await _tokenSource.Token.WaitForUpdate() == false)
			{
				Rotation -= _rotationSpeed;

				if (Rotation % 360 == 0)
				{
					ResetRotation();
				}
			}
		}

		private void ResetRotation() => Rotation = 0f;
	}
}