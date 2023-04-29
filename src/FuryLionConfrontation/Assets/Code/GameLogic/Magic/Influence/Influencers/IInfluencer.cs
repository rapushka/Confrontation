namespace Confrontation
{
	public interface IInfluencer
	{
		InfluenceStatus Status { get; }

		float Influence(float on, InfluenceTarget withTarget);
	}
}