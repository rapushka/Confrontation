using Zenject;

namespace Confrontation
{
	public abstract class OnOurBuildingsInfluencer<TBuilding> : ConditionalInfluencer<TBuilding>
		where TBuilding : Building
	{
		[Inject] private readonly User _user;

		protected override bool IsMatchCondition(TBuilding item) => item.OwnerPlayerId == _user.PlayerId;
	}
}