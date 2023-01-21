namespace Confrontation
{
	public static class Constants
	{
		public const float HexagonWidth = 1f;
		public const float HorizontalOffsetForOddRows = HexagonWidth / 2;

		public static class SceneName
		{
			public const string BootstrapScene = "BootstrapScene";
			public const string GameplayScene = "GameplayScene";
		}

		public static class Identifiers
		{
			public const string CurrentLevel = nameof(CurrentLevel);
		}
	}
}