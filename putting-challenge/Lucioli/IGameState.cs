using putting_challenge.Fantilli;
using System;
using System.Collections.Generic;

namespace putting_challenge.Lucioli
{
	public interface IGameState : Colleague
	{
		/// <summary>Initialize the state.</summary>
		/// <returns>returns a <see cref="Tuple"/> that contains the <see cref="SceneType"/> and a <see cref="List{GameObject}"/>.</returns>
		public Tuple<IEnumerable<SceneType>, IList<IGameObject>> InitState();

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

