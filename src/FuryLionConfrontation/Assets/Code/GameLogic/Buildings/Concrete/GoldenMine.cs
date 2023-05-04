using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GoldenMine : ResourcesGenerator
	{
		[Inject] private readonly InfluenceMediator _influence;

		public override string Name => nameof(GoldenMine);

		protected override LeveledStats<GeneratorStatsBase> Stats => StatsTable.GoldenMineStats.LeveledStats;

		protected override GeneratorStatsBase CurrentLevelStats
		{
			get
			{
				var generatorStatsBase = base.CurrentLevelStats;
				return new GoldenMineStatsDecorator(generatorStatsBase, _influence, this);
			}
		}

		protected override void Produce() => OwnerPlayer?.Resources.Gold.Earn(ProducingRate);
	}

	public class GoldenMineStatsDecorator : GeneratorStatsBase
	{
		private readonly InfluenceMediator _influence;
		private readonly GoldenMine _mine;
		private readonly GeneratorStatsBase _decoratee;

		public GoldenMineStatsDecorator(GeneratorStatsBase decoratee, InfluenceMediator influence, GoldenMine mine)
		{
			_decoratee = decoratee;
			_influence = influence;
			_mine = mine;
		}

		public override int Amount
		{
			get
			{
				var baseAmount = _decoratee.Amount;
				return Mathf.RoundToInt(_influence.Influence(baseAmount, InfluenceTarget.GoldenMineProduceRate, _mine));
			}
		}

		public override float CoolDown
		{
			get
			{
				var baseCoolDown = _decoratee.CoolDown;
				return _influence.Influence(baseCoolDown, InfluenceTarget.GoldenMineCooldown, _mine);
			}
		}
	}
}