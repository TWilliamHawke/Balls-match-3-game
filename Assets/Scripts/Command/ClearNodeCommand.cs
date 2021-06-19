using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBallCommand : Icommand
{
    Node _node;
    Color _ballColor;

    public RemoveBallCommand(Node node)
    {
        _node = node;
        _ballColor = node.ballColor;
    }

    public void Execute()
    {
        BallsPool.instance.HideBallFromNode(_node);
    }

    public void Undo()
    {
        BallSpawner.instance.SpawnBallInNode(_ballColor, _node);
    }
}
