using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisReader
{
    bool _keyIsPressed = false;
    string _axisName;

    System.Action _inputEvent;

    public AxisReader(string axisName, Action callback)
    {
        _axisName = axisName;
        _inputEvent = callback;
    }

    //update is reserved
    public void Tick()
    {
        var submitvalue = Input.GetAxis(_axisName);
        if(submitvalue > 0.5f && _keyIsPressed == false)
        {
            _keyIsPressed = true;
            _inputEvent?.Invoke();
        }

        if (submitvalue == 0 && _keyIsPressed)
        {
            _keyIsPressed = false;
        }
    }
}
