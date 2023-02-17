namespace Confrontation
{
	public interface IDefenceStrategy
	{
		int  Quantity { get; }
		void Destroy();
		void TakeDamage(int damage);
	}
}