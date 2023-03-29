using System.Linq;
using UnityEngine;

namespace Confrontation
{
	public class BordersContainer : MonoBehaviour
	{
		[SerializeField] private Cell _cell;

		[field: Header("Borders")]
		[SerializeField] private Border _left;
		[SerializeField] private Border _leftTop;
		[SerializeField] private Border _rightTop;
		[SerializeField] private Border _right;
		[SerializeField] private Border _rightBottom;
		[SerializeField] private Border _leftBottom;

		private BorderInfo[] _borderInfos;

		private bool IsOnEvenRow => _cell.Coordinates.Row.IsEven();

		private void OnEnable()
		{
			// https://www.redblobgames.com/grids/hexagons/#neighbors-offset — magic numbers explanation (greed type: odd-r)
			_borderInfos = new[]
			{
				new BorderInfo(forEven: On(+0, -1), forOdd: On(+0, -1), border: _left),
				new BorderInfo(forEven: On(+1, -1), forOdd: On(+1, +0), border: _leftTop),
				new BorderInfo(forEven: On(+1, +0), forOdd: On(+1, +1), border: _rightTop),
				new BorderInfo(forEven: On(+0, +1), forOdd: On(+0, +1), border: _right),
				new BorderInfo(forEven: On(-1, +0), forOdd: On(-1, +1), border: _rightBottom),
				new BorderInfo(forEven: On(-1, -1), forOdd: On(-1, +0), border: _leftBottom),
			};

			HideAll();
		}

		public void HideAll() => _borderInfos.Select((bi) => bi.Border).ForEach((b) => b.Hide());

		public void SetBorderFor(Cell otherCell)
		{
			if (_cell.RelatedRegion != otherCell.RelatedRegion)
			{
				_borderInfos.ForEach(ShowBorder, @if: (bi) => IsNeighbour(otherCell, bi));
			}
		}

		private static Coordinates On(int row, int column) => new(row: row, column: column);

		private static void ShowBorder(BorderInfo borderInfo) => borderInfo.Border.Show();

		private bool IsNeighbour(Cell otherCell, BorderInfo borderInfo)
			=> CurrentDelta(otherCell) == GetActualDelta(borderInfo);

		private Coordinates CurrentDelta(Cell otherCell) => otherCell.Coordinates - _cell.Coordinates;

		private Coordinates GetActualDelta(BorderInfo borderInfo)
			=> IsOnEvenRow ? borderInfo.ForEven : borderInfo.ForOdd;
	}
}