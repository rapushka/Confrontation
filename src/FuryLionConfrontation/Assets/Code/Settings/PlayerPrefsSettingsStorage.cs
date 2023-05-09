using UnityEngine;

namespace Confrontation
{
	public interface ISettingsStorage
	{
		float MusicVolume  { get; set; }
		float SoundsVolume { get; set; }
	}

	public class PlayerPrefsSettingsStorage : ISettingsStorage
	{
		public float MusicVolume
		{
			get => PlayerPrefs.GetFloat(nameof(ISettingsStorage.MusicVolume));
			set => PlayerPrefs.SetFloat(nameof(ISettingsStorage.MusicVolume), value);
		}

		public float SoundsVolume
		{
			get => PlayerPrefs.GetFloat(nameof(ISettingsStorage.SoundsVolume));
			set => PlayerPrefs.SetFloat(nameof(ISettingsStorage.SoundsVolume), value);
		}
	}
}