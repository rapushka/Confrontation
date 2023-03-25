using System;
using UnityEngine;
using UnityEngine.UI;

namespace Confrontation
{
	public abstract class EntryBase : ButtonBase
	{
		[SerializeField] private Image _selectionImage;

		public event Action<EntryBase> Clicked;

		private bool Selected { set => _selectionImage.enabled = value; }

		private void Start() => Deselect();

		protected override void OnButtonClick()
		{
			Clicked?.Invoke(this);
			Selected = true;
		}

		public void Select() => Selected = true;

		public void Deselect() => Selected = false;
	}
}