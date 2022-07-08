using putting_challenge.Fantilli;

namespace PuttingChallenge.Colletta.Mediator
{
    public interface IMediator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newColleague"></param>
        void AddColleague(Colleague newColleague);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toRemove"></param>
        void RemoveColleague(Colleague toRemove);

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        void NotifyColleagues(IGameEvent event, IColleague sender);
    }
}
