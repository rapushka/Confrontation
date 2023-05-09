namespace Confrontation
{
	public interface ISoundService
	{
		void BuildingBuilt();

		void BuildingUpgraded();

		void UnitStep();

		void UnitsFight();

		void SpellCast();

		void EndOfSpell();

		void Victory();

		void Loose();

		void UiClick();
	}
}