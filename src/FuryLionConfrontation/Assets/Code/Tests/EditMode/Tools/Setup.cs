namespace Confrontation.Editor.Tests
{
	public static class Setup
	{
		public static User User(int id = 1)
		{
			var user = Create.User();
			user.SetPrivateField("_playerId", id);
			return user;
		}
	}
}