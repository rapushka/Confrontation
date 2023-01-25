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
	}
}