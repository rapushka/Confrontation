namespace Confrontation
{
	public class Farm : Building
	{
		public override string Name => nameof(Farm);

		public override int UpgradePrice => throw new System.NotImplementedException();

		protected override int MaxLevel => throw new System.NotImplementedException();
	}
}