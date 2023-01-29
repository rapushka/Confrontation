using UnityEngine;

namespace Confrontation
{
	public class UnitAnimator : MonoBehaviour
	{
		[SerializeField] private Animator _animator;

		private static readonly int IsMoving = Animator.StringToHash("IsMoving");

		public void StartMoving() => ToggleMoving(true);

		public void StopMoving() => ToggleMoving(false);

		private void ToggleMoving(bool isMoving) => _animator.SetBool(IsMoving, isMoving);
	}
}