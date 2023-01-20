using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(menuName = "Confrontation/Level", fileName = "Level")]
	public class Level : ScriptableObject
	{
		[field: SerializeField] public Sizes Sizes { get; set; }

		[field: SerializeField] public List<Coordinates> VillagesCoordinates { get; private set; }
	}
}