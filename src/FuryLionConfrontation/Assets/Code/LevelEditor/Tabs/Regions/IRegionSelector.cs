namespace Confrontation
{
	public interface IRegionSelector
	{
		bool HasSelectedEntry { get; }

		Region SelectedRegion { get; }
	}
}