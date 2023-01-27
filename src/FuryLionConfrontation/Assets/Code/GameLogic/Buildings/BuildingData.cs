using System;

namespace Confrontation
{
	[Serializable]
	public class BuildingData
	{
		public Building    Prefab      { get; set; }
		public Coordinates Coordinates { get; set; }
	}
}