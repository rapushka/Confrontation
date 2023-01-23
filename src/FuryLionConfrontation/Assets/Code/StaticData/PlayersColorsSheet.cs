using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = "PlayersColorsSheet", menuName = nameof(Confrontation) + "/PlayersColorsSheet")]
	public class PlayersColorsSheet : ScriptableObject
	{
		[SerializeField] private List<Color> _colors;

		public Color GetColorFor(int playerId) => _colors[playerId];
	}
}