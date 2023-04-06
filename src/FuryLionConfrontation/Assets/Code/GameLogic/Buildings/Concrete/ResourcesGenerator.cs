using Zenject;

namespace Confrontation
{
	public abstract class ResourcesGenerator : Generator
	{
		[Inject] private readonly ISession _gameSession;

		public override float PassedDuration { get; set; }

		protected Player OwnerPlayer => _gameSession.GetPlayerFor(OwnerPlayerId);

		public override void Action() => Produce();

		protected abstract void Produce();
	}
}