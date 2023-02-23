namespace Confrontation
{
	public class Enemy
	{
		public void Construct(Player player)
		{
			Player = player;
		}

		public Player Player { get; private set; }
	}
}