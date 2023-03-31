using Zenject;

namespace Confrontation
{
	public class GameplayCameraSwipeMovement : CameraSwipeMovement
	{
		[Inject] private readonly OrderDirectionLineDrawer _orderDrawer;

		protected override bool IsSupposeToSwipe => base.IsSupposeToSwipe && _orderDrawer.IsGivingOrder == false;

		protected override void OnSwipeEnd()
		{
			base.OnSwipeEnd();

			if (_orderDrawer.IsGivingOrder)
			{
				PreventMoving();
			}
		}
	}
}