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
		private const string SoundsVolumePref = nameof(SoundsVolume);
		private const string MusicVolumePref = nameof(MusicVolume);

		public float MusicVolume
		{
			get
			{
				if (PlayerPrefs.HasKey(MusicVolumePref) == false)
				{
					MusicVolume = Constants.Audio.DefaultVolume.Music;
				}

				return PlayerPrefs.GetFloat(MusicVolumePref);
			}
			set => PlayerPrefs.SetFloat(MusicVolumePref, value);
		}

		public float SoundsVolume
		{
			get
			{
				if (PlayerPrefs.HasKey(SoundsVolumePref) == false)
				{
					SoundsVolume = Constants.Audio.DefaultVolume.Sound;
				}

				return PlayerPrefs.GetFloat(SoundsVolumePref);
			}
			set => PlayerPrefs.SetFloat(SoundsVolumePref, value);
		}
	}
}