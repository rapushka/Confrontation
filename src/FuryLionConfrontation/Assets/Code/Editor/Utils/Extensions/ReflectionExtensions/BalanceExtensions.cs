using System.Reflection;
using static System.Reflection.BindingFlags;

namespace Confrontation.Editor
{
	public static class BalanceExtensions
	{
		private const string GenerationAmount = "GenerationAmount";

		public static void SetGenerationAmount<T>(this object @this, T value)
		{
			var type = @this.GetType().BaseType;
			const BindingFlags flags = Public | NonPublic | SetProperty | Instance;
			type!.InvokeMember(GenerationAmount, flags, binder: null, target: @this, args: new object[] { value });
		}
	}
}