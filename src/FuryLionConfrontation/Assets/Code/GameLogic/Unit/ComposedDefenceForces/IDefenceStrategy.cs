namespace Confrontation
{
	public interface IDefenceStrategy
	{
		float BaseDamage { get; }

		int QuantityOfUnits { get; set; }

		void Destroy();

		void TakeDamageOnDefence(float incomingDamage);
	}
}