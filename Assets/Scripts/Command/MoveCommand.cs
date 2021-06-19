public class MoveCommand: Icommand
{
    Node _startNode;
    Node _targetNode;

    public MoveCommand(Node startNode, Node targetNode)
    {
        _startNode = startNode;
        _targetNode = targetNode;
    }

    public void Execute()
    {
        BallMoveController.instance.StartMovement(_startNode, _targetNode);
    }

    public void Undo()
    {
        var ball = _targetNode.ball;
        if(ball == null) {
            return;
        }

        _targetNode.RemoveBall();
        _startNode.AddBall(ball);
    }
}
