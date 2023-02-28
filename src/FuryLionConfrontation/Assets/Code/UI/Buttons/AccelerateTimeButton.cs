using Confrontation.GameLogic;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class AccelerateTimeButton : ButtonBase
	{
		[Inject] private readonly AccelerateableTimeServiceDecorator _timeService;

		[SerializeField] private float _accelerationCoefficient = 3f;

		protected override void OnButtonClick()
			=> _timeService.AccelerationCoefficient = _timeService.AccelerationCoefficient < _accelerationCoefficient
				? _accelerationCoefficient
				: 1f;
	}
}