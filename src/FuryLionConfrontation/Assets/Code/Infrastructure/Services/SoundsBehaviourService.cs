using UnityEngine;

namespace Confrontation
{
	public class SoundsBehaviourService : MonoBehaviour, ISoundService
	{
		[SerializeField] private AudioSource _audioSource;
		[Header("clips")]
		[SerializeField] private AudioClip _buildingBuilt;
		[SerializeField] private AudioClip _buildingUpgraded;
		[SerializeField] private AudioClip _error;

		public void BuildingBuilt(float volume = 1) => _audioSource.PlayOneShot(_buildingBuilt, volume);

		public void BuildingUpgraded(float volume = 1) => _audioSource.PlayOneShot(_buildingUpgraded, volume);

		public void UnitStep(float volume = 1)
		{
			throw new System.NotImplementedException();
		}

		public void UnitsFight(float volume = 1)
		{
			throw new System.NotImplementedException();
		}

		public void SpellCast(float volume = 1)
		{
			throw new System.NotImplementedException();
		}

		public void EndOfSpell(float volume = 1)
		{
			throw new System.NotImplementedException();
		}

		public void Victory(float volume = 1)
		{
			throw new System.NotImplementedException();
		}

		public void Loose(float volume = 1)
		{
			throw new System.NotImplementedException();
		}

		public void UiClick(float volume = 1)
		{
			throw new System.NotImplementedException();
		}

		public void UiError(float volume = 1) => _audioSource.PlayOneShot(_error, volume);
	}
}