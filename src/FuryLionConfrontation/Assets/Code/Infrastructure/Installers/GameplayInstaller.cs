using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameplayInstaller : MonoInstaller
	{
		[SerializeField] private List<WindowBase> _windows;

		// ReSharper disable Unity.PerformanceAnalysis - Method call only on initialization
		public override void InstallBindings()
		{
			var typedDictionary = new TypedDictionary<WindowBase>(_windows);

			Container.Bind<TypedDictionary<WindowBase>>().FromInstance(typedDictionary).AsSingle();

			Container.BindInterfacesAndSelfTo<Field>().AsSingle();
			Container.BindInterfacesAndSelfTo<Regions>().AsSingle();
			Container.BindInterfacesAndSelfTo<FieldClicksHandler>().AsSingle();
		}
	}
}