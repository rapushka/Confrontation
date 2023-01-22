using System;
using Zenject;

namespace Confrontation
{
	public static class SignalsBindingExtensions
	{
		public static DiContainer BindSignalTo<TSignal, TObject>
			(this DiContainer @this, Func<TObject, Action> handlerGetter)
		{
			@this.DeclareSignal<TSignal>();
			@this.BindSignal<TSignal>().ToMethod(handlerGetter).FromResolve();

			return @this;
		}

		public static DiContainer BindSignalTo<TSignal, TObject>
			(this DiContainer @this, Action<TObject, TSignal> handlerGetter)
		{
			@this.DeclareSignal<TSignal>();
			@this.BindSignal<TSignal>().ToMethod(handlerGetter).FromResolve();

			return @this;
		}
	}
}