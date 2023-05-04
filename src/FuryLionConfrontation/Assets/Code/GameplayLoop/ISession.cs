namespace Confrontation
{
	public interface ISession
	{
		void   AddPlayer(Player player);
		Player GetPlayerFor(int id);
		void   PlayerLoose(int looserId);
	}
}