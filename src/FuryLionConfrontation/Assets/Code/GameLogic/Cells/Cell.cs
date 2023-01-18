using JetBrains.Annotations;

namespace Code
{
	public class Cell
	{
		[CanBeNull] private Player _owner;
		[CanBeNull] private Building _building;

		public bool IsNeutral => _owner is null;

		public bool IsEmpty => _building is null;
	}
}