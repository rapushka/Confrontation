using System;
using System.Collections.Generic;
using UnityEditor;
using Zenject;

namespace Confrontation.Editor
{
	public class ConfigurableField : IField, IGuiRenderable
	{
		[Inject] private State _state;

		public CoordinatedMatrix<Cell>       Cells        { get; private set; }
		public CoordinatedMatrix<Building>   Buildings    { get; private set; }
		public CoordinatedMatrix<UnitsSquad> LocatedUnits { get; private set; }
		public CoordinatedMatrix<Garrison>   Garrisons    { get; private set; }
		public CoordinatedMatrix<Region>     Regions      { get; private set; }
		public List<Player>                  Players      { get; private set; }

		public RegionsNeighboring Neighboring { get; private set; }

		public void GuiRender()
		{
			EditorGUILayoutUtils.AsHorizontalGroup(Height);
			EditorGUILayoutUtils.AsHorizontalGroup(Width);

			Cells = new CoordinatedMatrix<Cell>(_state.Sizes);
			Buildings = new CoordinatedMatrix<Building>(_state.Sizes);
			LocatedUnits = new CoordinatedMatrix<UnitsSquad>(_state.Sizes);
			Garrisons = new CoordinatedMatrix<Garrison>(_state.Sizes);
			Regions = new CoordinatedMatrix<Region>(_state.Sizes);
			Players = new List<Player>();
			Neighboring = new RegionsNeighboring(_state.Sizes);
		}

		private void Height()
		{
			EditorGUILayout.PrefixLabel(nameof(_state.Sizes.Height));
			_state.Sizes.Height = EditorGUILayout.IntField(_state.Sizes.Height);
		}

		private void Width()
		{
			EditorGUILayout.PrefixLabel(nameof(_state.Sizes.Width));
			_state.Sizes.Width = EditorGUILayout.IntField(_state.Sizes.Width);
		}

		[Serializable]
		public class State
		{
			public Sizes Sizes;
		}
	}
}