using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class SpellCaster
	{
		[Inject] private readonly User _user;
		[Inject] private readonly ActiveInfluences _influences;

		private Resource UserMana => _user.Player.Resources.Mana;

		public void Cast(ISpell spell)
		{
			var isEnoughMana = UserMana.IsEnoughFor(spell.ManaCoast);

			if (isEnoughMana == false)
			{
				Debug.Log("TODO: Not enough mana window");
				return;
			}

			UserMana.Spend(spell.ManaCoast);
			_influences.CastSpell(spell);
		}
	}
}