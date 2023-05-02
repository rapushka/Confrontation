using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class OnAllMovingUnitsInfluencer : OnCollectionInfluencer<UnitsSquad>
	{
		private HashSet<UnitsSquad> InfluencedElements { get; set; }

		protected override IEnumerable<UnitsSquad> Collection => InfluencedElements;

		protected override InfluenceStatus CheckCondition()
			=> Collection.WithoutNulls().Any() ? InfluenceStatus.Neutral : InfluenceStatus.ForceDeath;

		public class Factory : PlaceholderFactory<IInfluencer, OnAllMovingUnitsInfluencer>
		{
			[Inject] private readonly IField _field;

			public override OnAllMovingUnitsInfluencer Create(IInfluencer decoratee)
			{
				var influencer = base.Create(decoratee);
				influencer.InfluencedElements = _field.MovingUnits.ToHashSet();
				return influencer;
			}
		}
	}
}