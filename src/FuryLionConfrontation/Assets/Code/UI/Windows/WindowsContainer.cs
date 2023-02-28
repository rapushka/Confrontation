using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class WindowsContainer : MonoBehaviour, IInitializable
	{
		[SerializeField] private List<WindowBase> _windows;

		public void Initialize() => Windows = new TypedDictionary<WindowBase>(_windows);

		public TypedDictionary<WindowBase> Windows { get; private set; }
	}
}