using UnityEngine;

namespace Confrontation
{
	public static class Constants
	{
		public const float HexagonWidth = 1f;
		public const float HorizontalOffsetForOddRows = HexagonWidth / 2;
		public const float HexagonHeight = 0.2f;

		public static class SceneName
		{
			public const string BootstrapScene = "BootstrapScene";
			public const string GameplayScene = "Scenes/GameplayScene";
			public const string MainMenuScene = "Scenes/MainMenuScene";
			public const string LevelEditorScene = "LevelEditorScene";
		}

		public const int NeutralRegion = 0;

		public static readonly Vector3 VerticalOffsetAboveCell = Vector3.up * HexagonHeight;

		public static class ResourcePath
		{
			public const string Cell = "Prefabs/Cell";
			public const string Garrison = "Prefabs/Units/Garrison";

			public const string GoldenMine = "Prefabs/Buildings/Golden Mine";
			public const string Barrack = "Prefabs/Buildings/Barracks";
			public const string Settlement = "Prefabs/Buildings/Settlement";
			public const string Capital = "Prefabs/Buildings/Capital";
			public const string Farm = "Prefabs/Buildings/Farm";
			public const string Stable = "Prefabs/Buildings/Stable";

			public const string ResourcesService = "ScriptableObjects/Resources";
			public const string TestLevel = "ScriptableObjects/For Tests/TestLevel";
		}
	}
}