using NSubstitute;
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
			var field = Substitute.For<IField>();
			field.Cells.Returns(new CoordinatedMatrix<Cell>(new Sizes(2, 4)));
			Container.Bind<IField>().FromInstance(field).AsSingle();

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
			_fieldGenerator.GetPrivateField<IField>("_field").Cells.Returns(NewField());
			
			_fieldGenerator.Initialize();
		}

		private CoordinatedMatrix<Cell> NewField() => new(_height, _width);
	}
}