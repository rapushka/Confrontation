namespace Confrontation
{
	public interface IInfluencer
	{
		float Influence(float on, InfluenceTarget withTarget);

		void CastSpell(ISpell spell);
	}
}