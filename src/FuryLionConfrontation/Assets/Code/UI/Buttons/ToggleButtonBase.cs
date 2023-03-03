namespace Confrontation
{
	public abstract class ToggleButtonBase : ButtonBase
	{
		private bool _clicked;

		protected override void OnButtonClick()
		{
			if (_clicked)
			{
				_clicked = false;
				OnToggleUnClicked();
			}
			else
			{
				_clicked = true;
				OnToggleClicked();
			}
		}

		protected abstract void OnToggleClicked();

		protected abstract void OnToggleUnClicked();
	}
}