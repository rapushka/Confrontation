using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;
using static Confrontation.Constants.Audio;

namespace Confrontation
{
	public class AudioSettingsSection : MonoBehaviour, IInitializable
	{
		[Inject] private readonly ISettingsStorage _settingsStorage;
		[Inject] private readonly AudioMixerGroup _audioMixerGroup;

		[SerializeField] private Slider _soundsSlider;
		[SerializeField] private Slider _musicSlider;

		public void Initialize()
		{
			_soundsSlider.value = _settingsStorage.SoundsVolume;
			_musicSlider.value = _settingsStorage.MusicVolume;
		}

		private void OnEnable()
		{
			_soundsSlider.onValueChanged.AddListener(ChangeSoundsVolume);
			_musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
		}

		private void OnDisable()
		{
			_soundsSlider.onValueChanged.RemoveListener(ChangeSoundsVolume);
			_musicSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
		}

		private void ChangeSoundsVolume(float value)
		{
			_settingsStorage.SoundsVolume = value;
			_audioMixerGroup.audioMixer.SetFloat(ExposedParameter.SoundsVolume, value.AsAudioVolume());
		}

		private void ChangeMusicVolume(float value)
		{
			_settingsStorage.MusicVolume = value;
			_audioMixerGroup.audioMixer.SetFloat(ExposedParameter.MusicVolume, value.AsAudioVolume());
		}
	}
}