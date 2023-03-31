namespace Confrontation
{
	public class BorderInfo
	{
		public BorderInfo(Coordinates forEven, Coordinates forOdd, Border border)
		{
			Border = border;
			ForEven = forEven;
			ForOdd = forOdd;
		}

		public Border Border { get; }

		public Coordinates ForEven { get; }

		public Coordinates ForOdd { get; }
	}
}