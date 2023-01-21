using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameplayInstaller : MonoInstaller
	{
		[SerializeField] private Cell _cellPrefab;
		[SerializeField] private Village _villagePrefab;
		[SerializeField] private Level _level;

		// ReSharper disable Unity.PerformanceAnalysis - Method call only on initialization
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<Field>().FromInstance(new Field(_level, _cellPrefab, _villagePrefab));
		}
	}
}