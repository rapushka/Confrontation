namespace Confrontation
{
	public interface ISoundService
	{
		void BuildingBuilt(float volume = 1f);
		void BuildingUpgraded(float volume = 1f);
		void UnitStep(float volume = 1f);
		void UnitsFight(float volume = 1f);
		void SpellCast(float volume = 1f);
		void EndOfSpell(float volume = 1f);
		void Victory(float volume = 1f);
		void Loose(float volume = 1f);
		void UiClick(float volume = 1f);
	}
}