using UnityEngine;
using Zenject;

namespace Confrontation
{
	[CreateAssetMenu(fileName = "User", menuName = nameof(Confrontation) + "/User", order = 0)]
	public class User : ScriptableObject, IInitializable
	{
		[SerializeField] private int _playerId = 1;

		public Player Player { get; private set; }

		public void Initialize() => Player = new Player(id: _playerId);
	}
}