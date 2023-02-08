using UnityEditor;

namespace Confrontation.Editor.Code.Editor
{
	[CustomPropertyDrawer(typeof(Coordinates))]
	public class CoordinatesPropertyDrawer : IntPairPropertyDrawerBase
	{
		protected override string NameFirst  => "_row";
		protected override string NameSecond => "_column";
	}
}