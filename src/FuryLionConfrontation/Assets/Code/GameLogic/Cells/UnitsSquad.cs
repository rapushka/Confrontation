using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class UnitsSquad : MonoBehaviour
	{
		public             Player Owner           { get; set; }
		public             Cell   LocationCell    { get; set; }
		[CanBeNull] public Cell   TargetCell      { get; set; }
		public             int    QuantityOfUnits { get; set; }
	}
}