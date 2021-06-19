using UnityEngine;


public class SpawnBallCommand : Icommand
{
    Node _node;
    Color _ballColor;

    public SpawnBallCommand(Node node, Color ballColor)
    {
        _node = node;
        _ballColor = ballColor;
    }

    public void Execute()
    {
        BallSpawner.instance.SpawnBallInNode(_ballColor, _node);
    }

    public void Undo()
    {
        BallsPool.instance.HideBallFromNode(_node);
    }
}
