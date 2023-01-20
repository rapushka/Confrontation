using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(menuName = "Confrontation/Level", fileName = "Level")]
	public class Level : ScriptableObject
	{
		[field: SerializeField] public Sizes Sizes { get; set; }

		[field: SerializeField] public List<Region> Regions { get; private set; } = new();
	}
}