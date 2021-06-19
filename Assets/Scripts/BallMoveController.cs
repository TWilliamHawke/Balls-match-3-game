using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoveController : Singleton<BallMoveController>
{
    [SerializeField] float _ballSpeed = 1;

    Ball _ball;
    Node _targetNode;
    Node _startNode;
    Node _currentTarget;
    Stack<Node> _path;

    //cache
    GameField _gameField;
    BallDestroyer _ballDestroyer;

    //events
    static public event System.Action OnMovementFinish;
    static public event System.Action OnMovementCancel;

    private void Start()
    {
        _gameField = FindObjectOfType<GameField>();
        _ballDestroyer = FindObjectOfType<BallDestroyer>();
    }

    public void StartMovement(Node startNode, Node targetNode)
    {
        _ball = startNode.ball;

        if(_ball == null) {
            return;
        }

        _targetNode = targetNode;
        _startNode = startNode;

        var pathFinder = new PathFinder(_gameField, _startNode, _targetNode);
        _path = pathFinder.FindPath();

        if (_path.Count > 0)
        {
            _currentTarget = _path.Pop();
        }
        else {
            OnMovementCancel?.Invoke();
        }
    }

    void FinishMovement()
    {
        _currentTarget = null;
        _startNode.RemoveBall();
        _targetNode.AddBall(_ball);
        _ballDestroyer.StartSearch(_targetNode);
        _ball = null;
        OnMovementFinish?.Invoke();
    }

    void Update()
    {
        if(_currentTarget == null) return;

        var distanceDelta = _ballSpeed * Time.deltaTime;
        var nextPos = Vector2.MoveTowards(_ball.transform.position, _currentTarget.position, distanceDelta);
        _ball.transform.position = nextPos;

        if(nextPos == _currentTarget.position)
        {
            if(_path.Count == 0) {
                FinishMovement();
            }
            else {
                _currentTarget = _path.Pop();
            }
        }
    }
}
