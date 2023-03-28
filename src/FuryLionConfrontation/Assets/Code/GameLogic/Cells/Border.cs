using UnityEngine;

namespace Confrontation
{
	public class Border : MonoBehaviour
	{
		private bool Visible { set => gameObject.SetActive(value); }

		public void Show() => Visible = true;

		public void Hide() => Visible = false;
	}
}