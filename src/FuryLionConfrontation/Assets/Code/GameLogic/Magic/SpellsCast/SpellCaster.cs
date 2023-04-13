using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class SpellCaster
	{
		[Inject] private readonly User _user;

		private Resource UserMana => _user.Player.Resources.Mana;

		public void Cast(ISpell spell)
		{
			var isEnoughFor = UserMana.IsEnoughFor(spell.ManaCoast);

			if (isEnoughFor == false)
			{
				Debug.Log("Not enough mana!");
				return;
			}
			
			UserMana.Spend(spell.ManaCoast);
			Debug.Log($"the {spell.Title} Spell was casted");
		}
	}
}