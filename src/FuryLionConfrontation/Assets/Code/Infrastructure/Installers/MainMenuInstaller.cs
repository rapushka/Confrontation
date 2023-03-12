using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace Confrontation
{
	public class MainMenuInstaller : MonoInstaller
	{
		[SerializeField] private PlayLevelButton _playLevelButtonPrefab;
		[SerializeField] private EditLevelButton _editLevelButtonPrefab;
		[SerializeField] private List<LevelScriptableObject> _levels;
		[SerializeField] private Transform _levelsGridRoot;
		[SerializeField] private LevelsForEditorPanel _levelsForEditorPanelPrefab;

		[SerializeField] private RectTransform _uiRoot;

		public override void InstallBindings()
		{
			Container.BindInstance<LevelButtonBase>(_playLevelButtonPrefab).AsSingle();

			InstallForLevelButtonsSpawner();

			Container.Bind<ToGameplay>().AsSingle();
			Container.Bind<ToLevelEditor>().AsSingle();
			Container.BindInterfacesTo<LevelButtonsSpawner>().AsSingle();

			Container.BindFactory<Building, Building, Building.Factory>().FromFactory<PrefabFactory<Building>>();

			Container.BindFactory<ILevel, LevelButtonBase, LevelButtonBase.Factory>()
			         .FromComponentInNewPrefab(_playLevelButtonPrefab);

			InstallLevelEditor();
		}

		private void InstallLevelEditor()
		{
#if UNITY_EDITOR
			Container.BindInstance(_levels).WhenInjectedInto<LevelsForEditorPanel>();

			Container.BindInterfacesAndSelfTo<LevelsForEditorPanel>()
			         .FromComponentInNewPrefab(_levelsForEditorPanelPrefab)
			         .UnderTransform(_uiRoot)
			         .AsSingle();

			Container.BindFactory<ILevel, LevelButtonBase, LevelButtonBase.Factory>()
			         .FromComponentInNewPrefab(_editLevelButtonPrefab)
			         .WhenInjectedInto<EditLevelButtonsSpawner>();

			Container.BindInterfacesAndSelfTo<EditLevelButtonsSpawner>().AsSingle();
#endif
		}

		private void InstallForLevelButtonsSpawner()
		{
			Container.BindInstance(_levelsGridRoot).WhenInjectedInto<LevelButtonsSpawner>();
			Container.BindInstance(_levels).WhenInjectedInto<LevelButtonsSpawner>();
		}
	}
}