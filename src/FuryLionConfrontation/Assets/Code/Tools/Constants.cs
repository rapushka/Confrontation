using UnityEngine;

namespace Confrontation
{
	public static class Constants
	{
		public const float MathDeviation = 0.01f;

		public const float HexagonWidth = 1f;
		public const float HorizontalOffsetForOddRows = HexagonWidth / 2;
		public const float HexagonHeight = 0.2f;

		public const float HoldButtonTimeBase = 0.25f;

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
			public const string TowerOfMages = "Prefabs/Buildings/Tower Of Mages";
			public const string Barrack = "Prefabs/Buildings/Barracks";
			public const string Settlement = "Prefabs/Buildings/Settlement";
			public const string Capital = "Prefabs/Buildings/Capital";
			public const string Farm = "Prefabs/Buildings/Farm";
			public const string Stable = "Prefabs/Buildings/Stable";
			public const string Forge = "Prefabs/Buildings/Forge";
			public const string Quarry = "Prefabs/Buildings/Quarry";
			public const string Workshop = "Prefabs/Buildings/Workshop";
			public const string Fort = "Prefabs/Buildings/Fort";

			public const string ResourcesService = "ScriptableObjects/Resources";
			public const string TestLevel = "ScriptableObjects/For Tests/TestLevel";
		}

		public static class Editor
		{
			public const int MaxIconPreviewHeight = 50;
		}

		public static class Exception
		{
			public const string ThereIsNoDefenders = "Defence strategy can't be picked for cell without defence forces";
		}

		public static class AnimationHash
		{
			public static readonly int IsMoving = Animator.StringToHash(Animation.IsMoving);
		}

		public static class Animation
		{
			public const string IsMoving = "IsMoving";
		}

		public static class Audio
		{
			public static class VolumeScale
			{
				public const float User = 1f;
				public const float Enemy = 0.25f;
			}
		}
	}
}