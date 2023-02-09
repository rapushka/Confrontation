using UnityEngine;

namespace Confrontation.Editor.Tests
{
	public static class Create
	{
		public static Coordinates Coordinates(int row = 0, int column = 0) => new(row, column);

		public static ResourcesService ResourcesService()
			=> Resources.Load<ResourcesService>(Constants.ResourcePath.ResourcesService);

		public static User User() => ScriptableObject.CreateInstance<User>();
	}
}