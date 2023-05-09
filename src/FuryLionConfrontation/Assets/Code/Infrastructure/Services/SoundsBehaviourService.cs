using UnityEngine;

namespace Confrontation
{
	public class SoundsBehaviourService : MonoBehaviour, ISoundService
	{
		[SerializeField] private AudioSource _audioSource;
		[Header("clips")]
		[SerializeField] private AudioClip _buildingBuilt;
		[SerializeField] private AudioClip _buildingUpgraded;
		[SerializeField] private AudioClip _unitsFight;
		[SerializeField] private AudioClip _spellCasted;
		[SerializeField] private AudioClip _victory;
		[SerializeField] private AudioClip _loose;
		[SerializeField] private AudioClip _error;

		public void BuildingBuilt(float volume = 1) => _audioSource.PlayOneShot(_buildingBuilt, volume);

		public void BuildingUpgraded(float volume = 1) => _audioSource.PlayOneShot(_buildingUpgraded, volume);

		public void UnitsFight(float volume = 1) => _audioSource.PlayOneShot(_unitsFight, volume);

		public void SpellCast(float volume = 1) => _audioSource.PlayOneShot(_spellCasted, volume);

		public void Victory(float volume = 1) => _audioSource.PlayOneShot(_victory, volume);

		public void Loose(float volume = 1) => _audioSource.PlayOneShot(_loose, volume);

		public void UiError(float volume = 1) => _audioSource.PlayOneShot(_error, volume);
	}
}