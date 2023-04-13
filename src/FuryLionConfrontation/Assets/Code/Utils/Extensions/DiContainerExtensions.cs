using UnityEngine;
using Zenject;

namespace Confrontation
{
	public static class DiContainerExtensions
	{
		public static void BindPrefabFactory<TContract, TFactory>(this DiContainer @this)
			where TFactory : PlaceholderFactory<Object, TContract>
			=> BindPrefabFactory<TContract, TFactory, PrefabFactory<TContract>>(@this);

		public static void BindPrefabFactory<TContract, TFactory, TActualFactory>(this DiContainer @this)
			where TFactory : PlaceholderFactory<Object, TContract>
			where TActualFactory : IFactory<Object, TContract>
			=> @this.BindFactory<Object, TContract, TFactory>().FromFactory<TActualFactory>();

		public static FromBinderGeneric<T> BindSelf<T>(this DiContainer @this) => @this.Bind<T>().To<T>();
		
		public static void Decorate<TContract, TDecoratee, TDecorator>(this DiContainer @this)
			where TDecorator : TContract
			where TDecoratee : TContract
			=> @this.Bind<TContract>().To<TDecoratee>().FromResolve().WhenInjectedInto<TDecorator>();
	}
}