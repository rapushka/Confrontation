using UnityEngine;

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
			public const string LevelEditorScene = "LevelEditorScene";
		}

		public const int NeutralRegion = 0;
		public const float Epsilon = 0.001f;
		public static readonly Vector3 VerticalOffsetAboveCell = Vector3.up * 0.2f;
	}
}