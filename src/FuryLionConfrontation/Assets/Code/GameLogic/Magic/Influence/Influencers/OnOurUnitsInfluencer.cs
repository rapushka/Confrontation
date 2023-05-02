using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class OnOurUnitsInfluencer : ConstrainedInfluencer<UnitsSquad>
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly User _user;

		protected override InfluenceStatus CheckCondition() => InfluenceStatus.Neutral;

		protected override IEnumerable<UnitsSquad> Collection
			=> _field.AllUnits.Where((u) => u.OwnerPlayerId == _user.PlayerId);

		public class Factory : PlaceholderFactory<IInfluencer, OnOurUnitsInfluencer> { }
	}
}