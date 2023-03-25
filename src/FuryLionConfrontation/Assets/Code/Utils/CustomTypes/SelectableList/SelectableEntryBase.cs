using System;
using UnityEngine;
using UnityEngine.UI;

namespace Confrontation
{
	public abstract class SelectableEntryBase : ButtonBase
	{
		[SerializeField] private Image _selectionImage;

		public event Action<SelectableEntryBase> EntrySelected;

		private bool Selected { set => _selectionImage.enabled = value; }

		private void Start() => Deselect();

		public void Select() => Selected = true;

		public void Deselect() => Selected = false;

		protected override void OnButtonClick()
		{
			Select();
			EntrySelected?.Invoke(this);
		}
	}
}