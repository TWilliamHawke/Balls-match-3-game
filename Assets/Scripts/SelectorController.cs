using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorController : MonoBehaviour
{
    [SerializeField] float _moveInterval = 0.5f;

    int _posX = 0;
    int _posY = 0;

    Coroutine _movement;
    Ball _selectedBall;

    SelectorState _currentState = SelectorState.ready;

    //cache
    GameField _gameField;
    CommandProcessor _commandProcessor;

    void Awake()
    {
        _gameField = FindObjectOfType<GameField>();
        _commandProcessor = FindObjectOfType<CommandProcessor>();

        BallDestroyer.OnSearchFinish += RestoreControl;
        BallMoveController.OnMovementCancel += SelectBallAgain;
        InputReader.OnSubmitKeyPress += TrySelectNode;
        InputReader.OnCursorNodeChange += MoveByMouse;
    }

    void OnDestroy()
    {
        BallDestroyer.OnSearchFinish -= RestoreControl;
        BallMoveController.OnMovementCancel -= SelectBallAgain;
        InputReader.OnSubmitKeyPress -= TrySelectNode;
        InputReader.OnCursorNodeChange -= MoveByMouse;
    }

    void Update()
    {
        if (_currentState == SelectorState.freezed)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        TryStartMovement(horizontal, vertical);
        TryStopMovement(horizontal, vertical);
    }

    public void MoveTo(int x, int y)
    {
        _posX = Mathf.Clamp(x, 0, 8);
        _posY = Mathf.Clamp(y, 0, 8);

        transform.position = new Vector2(_posX, _posY);
    }

    public void StopKeyboardMovement()
    {
        if (_movement != null)
        {
            StopCoroutine(_movement);
        }
        if (_currentState != SelectorState.freezed)
        {
            _currentState = SelectorState.ready;
        }
    }

    void TryStartMovement(float horizontal, float vertical)
    {
        if (CanGoHorizontal(horizontal))
        {
            int deltaX = (int)Mathf.Sign(horizontal);
            _movement = StartCoroutine(MoveByKeyboard(deltaX, 0));
            _currentState = SelectorState.moveHorizontal;
        }
        else if (CanGoVertical(vertical))
        {
            int deltaY = (int)Mathf.Sign(vertical);
            _movement = StartCoroutine(MoveByKeyboard(0, deltaY));
            _currentState = SelectorState.moveVertical;
        }

        bool CanGoVertical(float vertical)
        {
            return vertical != 0
                && _currentState != SelectorState.moveHorizontal
                && _currentState != SelectorState.moveVertical;
        }

        bool CanGoHorizontal(float horizontal)
        {
            return horizontal != 0
                && _currentState != SelectorState.moveHorizontal
                && _currentState != SelectorState.moveVertical;
        }
    }


    void TryStopMovement(float horizontal, float vertical)
    {
        if (CanStopHorizontal() || CanStopVertical())
        {
            StopKeyboardMovement();
        }

        bool CanStopHorizontal() => horizontal == 0 && _currentState == SelectorState.moveHorizontal;
        bool CanStopVertical() => vertical == 0 && _currentState == SelectorState.moveVertical;
    }

    void TrySelectNode()
    {
        bool CanSelectNode() => _currentState == SelectorState.ready; //&& Input.GetAxis("Submit") > 0.5;

        if (!CanSelectNode())
        {
            return;
        }

        Node node = _gameField.GetNode(_posX, _posY);

        if (node == null)
        {
            Debug.LogError("Node Doest't exist!!");
            return;
        }

        if (!node.IsEmpty())
        {
            SelectNewBall(node.ball);
        }
        else if (_selectedBall != null)
        {
            MoveBall(node);
        }
    }

    void SelectNewBall(Ball newBall)
    {
        _selectedBall?.Deselect();
        _selectedBall = newBall;
        _selectedBall.Select();
        _currentState = SelectorState.readyForMove;
    }

    void MoveBall(Node node)
    {
        _currentState = SelectorState.freezed;
        _selectedBall.Deselect();
        var moveCommand = new MoveCommand(_selectedBall.node, node);
        _commandProcessor.ExecuteCommand(moveCommand);
    }

    void RestoreControl()
    {
        _currentState = SelectorState.ready;
        _selectedBall = null;
    }

    void SelectBallAgain()
    {
        _currentState = SelectorState.readyForMove;
        _selectedBall.Select();
    }

    IEnumerator MoveByKeyboard(int deltaX, int deltaY)
    {
        while (true)
        {
            MoveTo(_posX + deltaX, _posY + deltaY);

            yield return new WaitForSeconds(_moveInterval);
        }
    }

    void MoveByMouse(int gridX, int gridY)
    {
        StopKeyboardMovement();
        MoveTo(gridX, gridY);
    }

    enum SelectorState
    {
        ready,
        moveHorizontal,
        moveVertical,
        readyForMove,
        freezed,
        controlledByMouse
    }

}
