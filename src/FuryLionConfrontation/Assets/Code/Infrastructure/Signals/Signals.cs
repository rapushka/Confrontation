namespace Confrontation
{
	// ReSharper disable ClassNeverInstantiated.Global - is Zenject signal
	public class ToggleCurtainSignal
	{
		public ToggleCurtainSignal(bool toEnable, bool immediately)
		{
			ToEnable = toEnable;
			Immediately = immediately;
		}

		public bool ToEnable { get; }

		public bool Immediately { get; }
	}

	public class LoadingCurtainShowImmediately { }

	public class LoadingCurtainHideImmediately { }

	public class LoadingCurtainShow { }

	public class LoadingCurtainHide { }
}