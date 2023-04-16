using UnityEngine;

namespace Confrontation
{
	public abstract class PrefixView<T> : View<T>
	{
		[SerializeField] private string _prefix;

		protected override string ValueToString => _prefix + base.ValueToString;
	}
}