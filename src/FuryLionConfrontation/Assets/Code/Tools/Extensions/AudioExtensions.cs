namespace Confrontation
{
	public static class AudioExtensions
	{
		public static float AsAudioVolume(this float @this)
			=> @this.Lerp(Constants.Audio.MinVolume, Constants.Audio.MaxVolume);
	}
}