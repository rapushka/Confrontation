using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class WindowsContainer : MonoBehaviour, IInitializable
	{
		[SerializeField] private List<WindowBase> _windows;

		private TypedDictionary<WindowBase> _typedDictionary;

		public void Initialize() => _typedDictionary = new TypedDictionary<WindowBase>(_windows);

		public TypedDictionary<WindowBase> Windows => _typedDictionary;
	}
}