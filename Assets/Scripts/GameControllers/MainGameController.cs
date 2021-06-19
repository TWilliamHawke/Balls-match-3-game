using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    GameField _gameField;
    BallSpawner _ballSpawner;
    GameRules _gameRules;

    const string BALLS_DATA = "balls_data";


    void Awake()
    {
        _gameField = FindObjectOfType<GameField>();
        _ballSpawner = FindObjectOfType<BallSpawner>();
        _gameRules = FindObjectOfType<GameRules>();

        BallDestroyer.OnSearchFinish += CheckEmptyNodes;
        InputReader.OnReturnKeyPress += SaveAndExit;
    }

    void OnDestroy()
    {
        BallDestroyer.OnSearchFinish -= CheckEmptyNodes;
        InputReader.OnReturnKeyPress -= SaveAndExit;
    }

    void Start()
    {
        if (SceneLoader.levelState == LevelState.loadedGame)
        {
            LoadGameData();
        }
        else
        {
            StartNewGame();
        }
    }

    void StartNewGame()
    {
        GameScore.ResetScore();
        SaveController.Clear(BALLS_DATA);
        _ballSpawner.NewGameSpawn();
    }

    void LoadGameData()
    {
        GameScore.LoadScore();
        LoadGameField();
        CommandProcessor.instance.ExecuteCommand(new SetNextBallsCommand(null));
    }

    public void SaveAndExit()
    {
        SaveGameField();
        GameScore.SaveScore();
        SceneLoader.LoadMainMenu();
    }

    void SaveGameField()
    {
        var ballsList = new List<BallData>();

        foreach(var node in _gameField.grid)
        {
            if (node.ball == null) continue;

            int colorIdx = _gameRules.GetColorIdx(node.ball.color);
            node.ball.GetPosition(out int x, out int y);
            var currentBallData = new BallData(colorIdx, x, y);
            ballsList.Add(currentBallData);
        }

        SaveController.Save<List<BallData>>(BALLS_DATA, ballsList);
    }

    void LoadGameField()
    {
        var ballsList = SaveController.Load<List<BallData>>(BALLS_DATA);
        foreach(var ballData in ballsList)
        {
            var color = _gameRules.GetColor(ballData.colorIdx);
            var node = _gameField.GetNode(ballData.posX, ballData.posY);
            _ballSpawner.SpawnBallInNode(color, node);
        }
    }

    void CheckEmptyNodes()
    {
        int count = _gameField.GetEmptyCells().Count;

        if(count == 0)
        {
            StartCoroutine(Defeat());
        }
    }


    IEnumerator Defeat()
    {
        yield return new WaitForSeconds(1);
        SceneLoader.LoadScoreScreen();
    }


}
