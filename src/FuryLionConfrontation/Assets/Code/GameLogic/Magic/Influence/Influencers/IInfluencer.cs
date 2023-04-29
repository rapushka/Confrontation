namespace Confrontation
{
	public interface IInfluencer
	{
		bool HasInfluenced { get; }

		float Influence(float on, InfluenceTarget withTarget);

		void CastSpell(ISpell spell);
	}
}