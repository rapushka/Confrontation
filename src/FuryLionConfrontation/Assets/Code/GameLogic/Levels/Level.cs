using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = "Level", menuName = nameof(Confrontation) + "/Level")]
	public class Level : ScriptableObject
	{
		[field: SerializeField] public int PlayersCount { get; private set; }

		[field: SerializeField] public Sizes Sizes { get; private set; }

		[field: SerializeField] public List<Region> Regions { get; private set; }
		[field: SerializeField] public List<Building.Data> Buildings { get; private set; }
	}
}