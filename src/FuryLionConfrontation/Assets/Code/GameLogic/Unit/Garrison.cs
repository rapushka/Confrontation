using TMPro;
using UnityEngine;

namespace Confrontation
{
	public class Garrison : MonoBehaviour, ICoordinated
	{
		[SerializeField] protected UnitAnimator _animator;
		[SerializeField] private TextMeshPro _quantityOfUnitsInSquadView;

		private int _quantityOfUnits;

		public Coordinates Coordinates { get; set; }

		public int QuantityOfUnits
		{
			get => _quantityOfUnits;
			set
			{
				_quantityOfUnits = value;
				_quantityOfUnitsInSquadView.text = value.ToString();
			}
		}
	}
}