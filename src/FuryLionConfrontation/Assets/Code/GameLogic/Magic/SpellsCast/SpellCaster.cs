using Zenject;

namespace Confrontation
{
	public class SpellCaster
	{
		[Inject] private readonly User _user;
		[Inject] private readonly ActiveInfluences _influences;

		private Resource UserMana => _user.Player.Resources.Mana;

		public bool TryCast(ISpell spell)
		{
			if (IsEnoughManaFor(spell) == false)
			{
				return false;
			}

			UserMana.Spend(spell.ManaCoast);
			_influences.CastSpell(spell);
			return true;
		}

		private bool IsEnoughManaFor(ISpell spell) => UserMana.IsEnoughFor(spell.ManaCoast);
	}
}