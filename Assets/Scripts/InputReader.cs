using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputReader : MonoBehaviour
{

    public static event Action OnSubmitKeyPress;
    public static event Action OnCancelKeyPress;
    public static event Action OnReturnKeyPress;
    public static event Action<int, int> OnCursorNodeChange;

    List<AxisReader> _readers = new List<AxisReader>();

    //State
    bool _cursorIsOutOfField = true;
    int MousePrevX = 0;
    int MousePrevY = 0;

    //cache
    SelectorController _selector;
    GameField _gameField;

    void Start()
    {
        _selector = FindObjectOfType<SelectorController>();
        _gameField = FindObjectOfType<GameField>();

        _readers.Add(new AxisReader("Cancel", InputReader.OnCancelKeyPress));
        _readers.Add(new AxisReader("Submit", InputReader.OnSubmitKeyPress));
        _readers.Add(new AxisReader("Return", InputReader.OnReturnKeyPress));
    }

    void Update()
    {
        ReadMousePosition();
        ReadMouseClick();

        foreach(var reader in _readers)
        {
            reader.Tick();
        }
    }

    void ReadMousePosition()
    {
        var mouseX = Input.mousePosition.x;
        var mouseY = Input.mousePosition.y;
        // UnSafe!!!
        var cursorGridPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var gridX = (int)Mathf.Floor(cursorGridPos.x);
        var gridY = (int)Mathf.Floor(cursorGridPos.y);


        if (_gameField.IsOutOfRange(gridX, gridY))
        {
            _cursorIsOutOfField = true;
        }
        else
        {
            _cursorIsOutOfField = false;
            TryMoveSelector(gridX, gridY);
        }

    }

    void TryMoveSelector(int gridX, int gridY)
    {
        bool IsAnotherNode = MousePrevX != gridX || MousePrevY != gridY;

        if (IsAnotherNode)
        {
            MousePrevX = gridX;
            MousePrevY = gridY;
            OnCursorNodeChange?.Invoke(gridX, gridY);
        }
    }

    void ReadMouseClick()
    {
        if (ClickWhenCursorOnField())
        {
            OnSubmitKeyPress?.Invoke();
        }

        bool ClickWhenCursorOnField()
        {
            return Input.GetMouseButtonDown(0) && _cursorIsOutOfField == false;
        }
    }
}
