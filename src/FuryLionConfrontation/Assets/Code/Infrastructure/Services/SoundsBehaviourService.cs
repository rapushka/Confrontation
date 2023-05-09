using UnityEngine;

namespace Confrontation
{
	public class SoundsBehaviourService : MonoBehaviour, ISoundService
	{
		[SerializeField] private AudioSource _audioSource;
		[Header("clips")]
		[SerializeField] private AudioClip _buildingBuilt;

		public void BuildingBuilt() => _audioSource.PlayOneShot(_buildingBuilt);

		public void BuildingUpgraded()
		{
			throw new System.NotImplementedException();
		}

		public void UnitStep()
		{
			throw new System.NotImplementedException();
		}

		public void UnitsFight()
		{
			throw new System.NotImplementedException();
		}

		public void SpellCast()
		{
			throw new System.NotImplementedException();
		}

		public void EndOfSpell()
		{
			throw new System.NotImplementedException();
		}

		public void Victory()
		{
			throw new System.NotImplementedException();
		}

		public void Loose()
		{
			throw new System.NotImplementedException();
		}

		public void UiClick()
		{
			throw new System.NotImplementedException();
		}
	}
}