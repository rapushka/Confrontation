using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class MainMenuInstaller : MonoInstaller
	{
		[SerializeField] private PlayButton _playButton;

		public override void InstallBindings()
		{
			Container.BindInstance(_playButton).AsSingle();

			Container.Bind<ToGameplay>().AsSingle();
		}
	}
}