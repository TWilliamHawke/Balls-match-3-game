using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : Singleton<BallSpawner>
{
    [SerializeField] List<Color> colorList = new List<Color>();

    Color[] nextBalls = new Color[3];

    //cache
    GameField _gameField;
    BallsPool _ballsPool;
    GameRules _gameRules;
    BallDestroyer _ballDestroyer;

    static public event System.Action<Color[]> OnUpdateBalls;

    bool _spawnIsSuspended = false;

    protected override void Awake() {
        base.Awake();
        BallMoveController.OnMovementFinish += SpawnThreeBalls;
        GameScore.OnScoreChange += SuspendSpawn;
        _gameField = FindObjectOfType<GameField>();
        _ballsPool = FindObjectOfType<BallsPool>();
        _gameRules = FindObjectOfType<GameRules>();
        _ballDestroyer = FindObjectOfType<BallDestroyer>();
    }

    private void OnDestroy() {
        BallMoveController.OnMovementFinish -= SpawnThreeBalls;
    }

    void SpawnThreeBalls()
    {
        foreach(var color in nextBalls)
        {
            SpawnBallRandomly(color);
        }
        _spawnIsSuspended = false;
        CommandProcessor.instance.ExecuteCommand(new SetNextBallsCommand(nextBalls));

    }

    public void NewGameSpawn()
    {
        for(int i = 0; i < _gameRules.startBallsCount; i++)
        {
            var color = _gameRules.GetRandomColor();
            SpawnBallRandomly(color);
        }
        CommandProcessor.instance.ExecuteCommand(new SetNextBallsCommand(nextBalls));

    }

    void SpawnBallRandomly(Color ballColor)
    {
        var emptyCells = _gameField.GetEmptyCells();
        bool SpawnShouldBeStop() => emptyCells.Count == 0 || (emptyCells.Count < 75 && _spawnIsSuspended);

        if (SpawnShouldBeStop())
        {
            return;
        }

        int randomIdx = Random.Range(0, emptyCells.Count);
        var spawnNode = emptyCells[randomIdx];

        var spawnCommand = new SpawnBallCommand(spawnNode, ballColor);
        CommandProcessor.instance.ExecuteCommand(spawnCommand);
        _ballDestroyer.StartSearch(spawnNode);
    }

    public void SpawnBallInNode(Color ballColor, Node node)
    {
        var ball = _ballsPool.GetBall();
        ball.color = ballColor;
        node.AddBall(ball);
        ball.transform.parent = transform;
    }

    public void SetNextBalls(Color[] colors)
    {
        nextBalls = colors;
        OnUpdateBalls?.Invoke(nextBalls);
    }

    void SuspendSpawn(int score)
    {
        if(score > 0) {
            _spawnIsSuspended = true;
        }
    }
}
