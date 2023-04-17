using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class UnitAnimator : MonoBehaviour
	{
		[Inject] private Animator _animator;

		private static readonly int _isMoving = Animator.StringToHash("IsMoving");

		public void StartMoving() => ToggleMoving(true);

		public void StopMoving() => ToggleMoving(false);

		private void ToggleMoving(bool isMoving) => _animator.SetBool(_isMoving, isMoving);
	}
}