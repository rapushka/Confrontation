using UnityEngine;

namespace Confrontation
{
	public class Border : MonoBehaviour
	{
		public bool Visible { set => gameObject.SetActive(value); }

		public void Show() => Visible = true;

		public void Hide() => Visible = false;
	}
}