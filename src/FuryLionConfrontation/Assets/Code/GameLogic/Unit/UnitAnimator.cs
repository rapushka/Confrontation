using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class UnitAnimator : MonoBehaviour
	{
		[Inject] private Animator _animator;

		public bool IsMoving
		{
			get => _animator.GetBool(Constants.AnimationHash.IsMoving);
			private set => _animator.SetBool(Constants.AnimationHash.IsMoving, value);
		}

		public void StartMoving() => IsMoving = true;

		public void StopMoving() => IsMoving = false;
	}
}