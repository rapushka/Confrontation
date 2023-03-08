using System.Linq;

namespace Confrontation
{
	public class InfluencedBarrackDecorator : Barrack
	{
		public override float CoolDownDuration
		{
			get
			{
				var ownerPlayerId = OwnerPlayerId;
				var influencers = Field.Buildings
				                       .OfType<Farm>()
				                       .Where((f) => f.OwnerPlayerId == ownerPlayerId);

				var baseDuration = base.CoolDownDuration;
				return influencers.Aggregate(baseDuration, (c, f) => c - f.CurrentLevelStats.IncreaseCoefficient);
			}
		}
	}
}