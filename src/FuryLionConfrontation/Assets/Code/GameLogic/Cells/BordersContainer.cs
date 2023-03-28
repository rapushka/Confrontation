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

		private void OnEnable()
		{
			_borderInfos = new[]
			{
				new BorderInfo(forEven: On(0, -1), forOdd: On(0, -1), border: _left),
				new BorderInfo(forEven: On(1, -1), forOdd: On(1, 0), border: _leftTop),
				new BorderInfo(forEven: On(1, 0), forOdd: On(1, 1), border: _rightTop),
				new BorderInfo(forEven: On(0, 1), forOdd: On(0, 1), border: _right),
				new BorderInfo(forEven: On(1, 0), forOdd: On(-1, 1), border: _rightBottom),
				new BorderInfo(forEven: On(-1, 1), forOdd: On(-1, 0), border: _leftBottom),
			};

			_borderInfos.Select((bi) => bi.Border).ForEach((b) => b.Hide());
		}

		private static Coordinates On(int row, int column) => new(row, column);

		private bool IsOnEvenRow => _cell.Coordinates.Row.IsEven();

		public void SetBorderFor(Cell otherCell)
		{
			if (_cell.RelatedRegion == otherCell.RelatedRegion)
			{
				return;
			}

			var delta = _cell.Coordinates - otherCell.Coordinates;

			_borderInfos.ForEach((bi) => bi.Border.Show(), @if: (bi) => delta == GetActualDelta(bi));
		}

		private Coordinates GetActualDelta(BorderInfo borderInfo)
			=> IsOnEvenRow ? borderInfo.ForEven : borderInfo.ForOdd;
	}
}