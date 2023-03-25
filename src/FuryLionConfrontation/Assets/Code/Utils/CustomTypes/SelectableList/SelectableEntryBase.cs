using System;
using UnityEngine;
using UnityEngine.UI;

namespace Confrontation
{
	public abstract class SelectableEntryBase : ButtonBase
	{
		[SerializeField] private Image _selectionImage;

		public event Action<SelectableEntryBase> Clicked;

		private bool Selected { set => _selectionImage.enabled = value; }

		private void Start() => Deselect();

		protected override void OnButtonClick()
		{
			Clicked?.Invoke(this);
			Select();
		}

		public void Select() => Selected = true;

		public void Deselect() => Selected = false;
	}
}