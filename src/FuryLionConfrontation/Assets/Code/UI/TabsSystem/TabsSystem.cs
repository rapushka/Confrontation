using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Confrontation
{
	using UnityEngine;

	public class TabsSystem : MonoBehaviour
	{
		[SerializeField] private List<Tab> _tabs;

		[CanBeNull] private Tab _currentTab;

		private void OnEnable() => _tabs.ForEach((t) => t.Button.onClick.AddListener(() => Open(t)));

		private void OnDisable() => _tabs.ForEach((t) => t.Button.onClick.RemoveAllListeners());

		private void Start()
		{
			_tabs.ForEach((t) => t.Close());
			Open(_tabs.First());
		}

		private void Open(Tab tab)
		{
			_currentTab?.Close();
			tab.Open();
			_currentTab = tab;
		}
	}
}