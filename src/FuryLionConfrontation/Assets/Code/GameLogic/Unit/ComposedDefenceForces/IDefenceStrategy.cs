namespace Confrontation
{
	public interface IDefenceStrategy
	{
		float BaseDamage { get; }

		int QuantityOfUnits { get; }

		float HealthPoints { get; }

		void Destroy();

		void TakeDamageOnDefence(float incomingDamage, float pierceRate);

		void Kill();
	}
}