using UnityEngine;

namespace Confrontation
{
	public class SpellButton : HoldButtonBase
	{
		protected override void HandleClick()
		{
			Debug.Log("Cast spell");
		}

		protected override void HandleHold()
		{
			Debug.Log("Show Spell Info");
		}
	}
}