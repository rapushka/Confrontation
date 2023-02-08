using UnityEditor;

namespace Confrontation.Editor.Code.Editor
{
	[CustomPropertyDrawer(typeof(Sizes))]
	public class SizesPropertyDrawer : IntPairPropertyDrawerBase
	{
		protected override string NameFirst  => "_width";
		protected override string NameSecond => "_height";
	}
}