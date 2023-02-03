using UnityEditor;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class LevelEditorWindow : ZenjectEditorWindow
	{
		private GameObject _rootForLevel;

		[MenuItem("Tools/" + nameof(Confrontation) + "/Level Editor")]
		private static void ShowWindow()
		{
			var window = GetWindow<LevelEditorWindow>();
			window.titleContent = new GUIContent(nameof(LevelEditorWindow));
			window.Show();
		}

		// ReSharper disable Unity.PerformanceAnalysis
		public override void InstallBindings()
		{
			var resourcesService = Resources.Load<ResourcesService>("ScriptableObjects/Resources");

			Container.Bind<FieldGenerator>().AsSingle();
			Container.Bind<IResourcesService>().FromInstance(resourcesService).AsSingle();
			Container.Bind<IAssetsService>().To<AssetsService>().AsSingle();
			Container.Bind<IField>().FromSubstitute();

			Container.BindInterfacesTo<LevelEditor>().AsSingle();
		}
	}

	public class LevelEditor : IGuiRenderable
	{
		[Inject] private readonly FieldGenerator _fieldGenerator;

		private int _height;
		private int _width;

		public void GuiRender()
		{
			_height = EditorGUILayout.IntField(_height);
			_width = EditorGUILayout.IntField(_width);

			GUILayout.Button(nameof(Generate).Format()).OnClick(Generate);
		}

		private void Generate()
		{
			_fieldGenerator.InvokePrivateMethod("CreateHexagon", _height, _width);
		}
	}
}