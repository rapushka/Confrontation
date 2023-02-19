namespace Confrontation
{
	public interface ILeveled<T>
		where T : IStats
	{
		LeveledStats<T> LeveledStats { get; }
	}
}