using System;

namespace OOP21-putting-challenge-csharp.Lucioli
{
	public interface IGameState
	{
		/// <summary>Initialize the state.</summary>
		/// <returns>returns a <see cref="Tuple"/> that contains the <see cref="SceneType"/> and a <see cref="List{GameObject}"/>.</returns>
		public Tuple<SceneType, List<IGameObject>> InitState();

		/// <summary>
		/// Sets the <see cref="Environment"/> of the state.
		/// </summary>
		/// <param name="environment">Is the environment to set.</param>
		public void SetEnvironment(Optional<IEnvironment> environment);

		/// <summary>
		/// Notify the intercepted model event.
		/// </summary>
		/// <param name="eventType">Is the type of event intercepted.</param>
		public void NotifyEvents(ModelEventType eventType);

		/// <summary>
		/// Reads the events received from the <see cref="Environment"/>.
		/// </summary>
		public void ReceiveEvents();

		/// <summary>
		/// The last operation to be done by the current state before ending.
		/// </summary>
		/// <param name="nextStatus"></param>
		public void LeavingState(GameStatus nextStatus);
	}
}

