namespace Confrontation
{
	public interface IDefenceStrategy
	{
		int DefenceStrength { get; }

		void Destroy();

		void TakeDamage(int damage);
	}
}