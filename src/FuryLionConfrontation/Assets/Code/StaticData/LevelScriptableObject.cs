using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = "Level", menuName = nameof(Confrontation) + "/Level")]
	public class LevelScriptableObject : ScriptableObject, ILevel
	{
		[field: SerializeField] public Sizes               Sizes     { get; private set; }
		[field: SerializeField] public List<Region.Data>   Regions   { get; private set; } = new();
		[field: SerializeField] public List<Building.Data> Buildings { get; private set; } = new();
	}
}