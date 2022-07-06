namespace OOP21-putting-challenge-csharp.Lucioli
{
    public class GamePlayGameState : GameState
{
    private const int None = 0;
    private const int MaxLives = 3;
    private const int MaxStrength = 600;

    private ObservableEvents<ModelEventType> _observable;
    private ObserverEvents<ModelEventType> _observer;
    private SceneType _currentScene;
    private Mediator _generalMediator;

    private static IEnumerable<SceneType> GetNextMap()
    {
        List<SceneType> maps = new List<SceneType>()
            {
                SceneType.Environment1, SceneType.Environment2, SceneType.Environment3
            };

        foreach (var map in maps)
        {
            yield return map;
        }
    }

    public int Score { get; }
    public int Lives { get; }

    /// <summary>
    /// Builds a new <see cref="GamePlayGameState"/> object.
    /// </summary>
    /// <param name="manager"> of the state.</param>
    /// <param name="status"> associated with the <see cref="GamePlayGameState"/> state.</param>
    public GamePlayGameState(GameStateManager manager, GameStatus status) : base(manager, status)
    {
    }

    /// <inheritdoc/>
    public override Tuple<SceneType, List<IGameObject>> initState()
    {
        Lives = MaxLives;
        Score = None;
        LoadNextEnvironment();
        return new Tuple<SceneType, List<IGameObject>>(_currentScene, Environment.GetObjects());
    }

    /// <inheritdoc/>
    public override LeavingState(GameStatus nextStatus)
    {
        WriteStats();
        base.LeavingState(nextStatus);
    }

    private void DecScore() => Score--;
    private void IncScore() => Score++;
    private void DecLives() => Lives--;
    private void IncLives() => Lives++;

    /// <summary>
    /// Method called when the ball enters the hole.
    /// </summary>
    private void HandleWin()
    {
        IncScore();
        LoadNextEnvironment();
    }

    /// <summary>
    /// Method called when the ball stops or is out of bound.
    /// </summary>
    private void HandleMiss()
    {
        DecLives();
        if (Lives == None)
        {
            LeavingState(GameStatus.GameOver);
        }
        else 
        { 
           
        }
    }

    /// <summary>
    /// Sets the velocity of the ball according to the aiming <see cref="Vector2D"/> created from the given <see cref="Point2D"/>.
    /// </summary>
    /// <param name="points"> where the mouse was pressed and released during the aiming phase.</param>
    public void Shoot(Tuple<Point2D, Point2D> points)
    {
        readonly BallPhysicsComponent ballPhysicsComponent = (BallPhysicsComponent)Environment.Ball.GetPhysicsComponent();
        readonly double batStrength = Environment.Player.Bat.Type.Strength;
        if (!ballPhysicsComponent.IsMoving)
        {
            Vector2D shootingVector = Vector2D.GetVectorFrom(points.Item1, points.Item2);
            shootingVector.SetX(shootingVector.X * batStrength);
            shootingVector.SetY(shootingVector.Y * batStrength);
            if(shootingVector.GetModule() > MaxStrength)
            {
                readonly double moduleRate= MaxStrength / shootingVector.GetModule();
                shootingVector = new Vector2D(moduleRate * shootingVector.X, moduleRate * shootingVector.Y);
            }
            Environment.Ball.SetVelocity(shootingVector);
            NotifyEvents(ModelEventType.Shoot);
        }
    }
}
}