using UnityEngine;
using Zenject;

namespace Confrontation
{
	[CreateAssetMenu(fileName = "User", menuName = nameof(Confrontation) + "/User")]
	public class User : ScriptableObject, IInitializable, ILevelSelector
	{
		[SerializeField] private int _playerId = 1;

		public Player Player { get; private set; }

		public void Initialize() => Player = new Player(id: _playerId);

		public ILevel SelectedLevel { get; set; }
	}
}