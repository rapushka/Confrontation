using System;
using UnityEngine;
using UnityEngine.UI;

namespace Confrontation
{
	[Serializable]
	public class Tab
	{
		public Tab(Button button, Page page)
		{
			Button = button;
			Page = page;
		}

		[field: SerializeField] public Button Button { get; private set; }
		[field: SerializeField] public Page   Page   { get; private set; }

		public void Open() => Page.gameObject.SetActive(true);

		public void Close() => Page.gameObject.SetActive(false);
	}
}
