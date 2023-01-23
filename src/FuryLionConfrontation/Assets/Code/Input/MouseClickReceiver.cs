using System;
using UnityEngine;

namespace Confrontation
{
	[RequireComponent(typeof(Collider))]
	public class MouseClickReceiver : MonoBehaviour
	{
		public event Action MouseClick; 
		
		private void OnMouseDown() => MouseClick?.Invoke();
	}
}