using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNextBallsCommand : Icommand
{
    Color[] _prevColors = new Color[3];

    public SetNextBallsCommand(Color[] prevColors)
    {
        _prevColors = prevColors;
    }

    public void Execute()
    {
        Color[] nextColors = new Color[3];
        for(var i = 0; i < nextColors.Length; i++)
        {
            nextColors[i] = GameRules.instance.GetRandomColor();
        }
        BallSpawner.instance.SetNextBalls(nextColors);
    }

    public void Undo()
    {
        BallSpawner.instance.SetNextBalls(_prevColors);
    }

}
