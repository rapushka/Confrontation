using UnityEngine;

namespace Confrontation
{
	public abstract class PrefixPostfixView<T> : PrefixView<T>
	{
		[SerializeField] private string _postfix;

		protected override string ValueToString => base.ValueToString + _postfix;
	}
}