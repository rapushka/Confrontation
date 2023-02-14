using System;

namespace Confrontation
{
	[Serializable]
	public class Player
	{
		public Player(int id) => Id = id;

		public Player(int id, Capital capital)
		{
			Capital = capital;
			Id = id;
		}

		public int Id { get; }

		public Capital Capital { get; }

		public PlayerStats Stats { get; set; } = new();
		
		public override bool Equals(object obj) => obj is Player player && Equals(player);

		public bool Equals(Player player) => GetHashCode() == player.GetHashCode();

		public override int GetHashCode() => Id;
	}
}