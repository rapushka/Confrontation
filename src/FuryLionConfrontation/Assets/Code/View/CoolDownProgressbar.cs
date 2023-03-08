using UnityEngine;
using UnityEngine.UI;

namespace Confrontation
{
	public class CoolDownProgressbar : MonoBehaviour
	{
		[SerializeField] private Generator _generator;
		[SerializeField] private Slider _scrollbar;

		private void Update()
		{
			_scrollbar.maxValue = _generator.CoolDownDuration;
			_scrollbar.value = _generator.PassedDuration;
		}
	}
}