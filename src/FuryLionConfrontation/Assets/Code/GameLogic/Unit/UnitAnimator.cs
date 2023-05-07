using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class UnitAnimator : MonoBehaviour
	{
		[Inject] private Animator _animator;
		[Inject] private readonly ITimeService _time;

		private const float AnimatorDefaultSpeed = 1.0f;
		private const float StoppedAnimatorSpeed = 0.0f;
		
		public bool IsMoving
		{
			get => _animator.GetBool(Constants.AnimationHash.IsMoving);
			private set => _animator.SetBool(Constants.AnimationHash.IsMoving, value);
		}

		private void Update() => _animator.speed = _time.DeltaTime > 0 ? AnimatorDefaultSpeed : StoppedAnimatorSpeed;

		public void StartMoving() => IsMoving = true;

		public void StopMoving() => IsMoving = false;
	}
}