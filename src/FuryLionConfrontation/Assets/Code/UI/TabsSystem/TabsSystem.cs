using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Confrontation
{
	using UnityEngine;

	public class TabsSystem : MonoBehaviour
	{
		[SerializeField] private List<Tab> _tabs;

		[CanBeNull] protected Tab CurrentTab;

		protected IEnumerable<Tab> Tabs => _tabs;

		private void OnEnable() => _tabs.ForEach(SubscribeOpenButton);

		private void OnDisable() => _tabs.ForEach((t) => t.Button.onClick.RemoveAllListeners());

		private void Start() => OpenOnlyFirstTab();

		public void AddTab(Tab tab)
		{
			SubscribeOpenButton(tab);
			_tabs.Add(tab);
			OpenOnlyFirstTab();
		}

		private void SubscribeOpenButton(Tab tab) => tab.Button.onClick.AddListener(() => Open(tab));

		private void OpenOnlyFirstTab()
		{
			if (_tabs.Any())
			{
				_tabs.ForEach((t) => t.Close());
				Open(_tabs.First());
			}
		}

		private void Open(Tab tab)
		{
			CurrentTab?.Close();
			tab.Open();
			CurrentTab = tab;
		}
	}
}