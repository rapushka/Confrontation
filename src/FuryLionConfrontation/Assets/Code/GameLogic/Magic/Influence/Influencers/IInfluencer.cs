namespace Confrontation
{
	public interface IInfluencer
	{
		bool IsAlive { get; }

		float Influence(float on, InfluenceTarget withTarget);
	}
}