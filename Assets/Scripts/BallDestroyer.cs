using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyer : Singleton<BallDestroyer>
{
    GameField _gameField;
    GameRules _gameRules;

    public static event System.Action OnSearchFinish;

    Node _centralNode;
    int _destroyedBalls = 1;

    List<Node> _nodeLine = new List<Node>();

    protected override void Awake()
    {
        _gameField = FindObjectOfType<GameField>();
        _gameRules = FindObjectOfType<GameRules>();
    }

    public void StartSearch(Node node)
    {
        _destroyedBalls = 1;
        _centralNode = node;
        CheckDirection(1, 0); //horizontal
        CheckDirection(0, 1); //vertical
        CheckDirection(1, 1); //diagonal 1
        CheckDirection(-1, 1); // diagonal 2

        if(_destroyedBalls > 1)
        {
            RemoveBall(_centralNode);
            int score = _gameRules.GetScore(_destroyedBalls);
            var addScoreCommand = new AddScoreCommand(score);
            CommandProcessor.instance.ExecuteCommand(addScoreCommand);
        }
        OnSearchFinish?.Invoke();
    }

    void RemoveBall(Node node)
    {
        var removeCommand = new RemoveBallCommand(node);
        CommandProcessor.instance.ExecuteCommand(removeCommand);
    }

    void CheckDirection(int stepX, int stepY)
    {
        _nodeLine.Clear();
        CheckNextNode(_centralNode, stepX, stepY);
        CheckNextNode(_centralNode, -stepX, -stepY);

        if (_nodeLine.Count >= 4)
        {
            _destroyedBalls += _nodeLine.Count;
            foreach(var node in _nodeLine)
            {
                RemoveBall(node);
            }
        }
    }

    void CheckNextNode(Node node, int stepX, int stepY)
    {
        Color ballColor = node.ballColor;

        Node nextNode = _gameField.GetNode(node.x + stepX, node.y + stepY);
        if (ballColor == nextNode?.ballColor)
        {
            _nodeLine.Add(nextNode);
            CheckNextNode(nextNode, stepX, stepY);
        }

    }

}
