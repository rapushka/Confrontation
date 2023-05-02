using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public abstract class ConstrainedInfluencer<T> : ConditionalInfluencer<T>
	{
		protected abstract IEnumerable<T> Collection { get; }
		
		protected override bool IsMatchCondition(T item) => Collection.Contains(item);

		public override float Influence(float baseValue, InfluenceTarget withTarget) => baseValue;
	}
}