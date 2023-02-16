using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class Village : Building, IActorWithCoolDown
	{
		[Inject] private readonly Garrison.Factory _garrisonsFactory;

		public float PassedDuration { get; set; }

		public override string Name => nameof(Village);

		protected override int MaxLevel => BalanceTable.Village.MaxLevel;

		public IEnumerable<Cell> CellsInRegion
		{
			get
			{
				var region = Field.Regions[Coordinates];

				var regionsCoordinates = Field.Regions.Where((r) => r == region)
				                              .Select((r) => r.Coordinates);

				foreach (var coordinates in regionsCoordinates)
				{
					yield return Field.Cells[coordinates];
				}
			}
		}

		public float CoolDownDuration => Balance.GenerationCoolDown;

		private VillageBalanceData Balance => BalanceTable.Village[Level];

		private bool HaveGarrison => LocatedGarrison is not null;

		private Garrison LocatedGarrison => Field.Garrisons[Coordinates];

		public void Action()
		{
			for (var i = 0; i < Balance.GenerationAmount; i++)
			{
				SpawnGarrison();
			}
		}

		private void SpawnGarrison()
		{
			if (HaveGarrison)
			{
				LocatedGarrison.QuantityOfUnits++;
			}
			else
			{
				_garrisonsFactory.Create(RelatedCell);
			}
		}
	}
}