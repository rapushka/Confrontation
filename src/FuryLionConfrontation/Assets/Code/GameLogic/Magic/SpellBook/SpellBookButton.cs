using UnityEngine;

namespace Confrontation
{
	public class SpellBookButton : ButtonBase
	{
		protected override void OnButtonClick()
		{
			Debug.Log("open Spell Book!");
		}
	}
}