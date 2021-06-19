using UnityEngine;

public class Node
{
    public int x { get; }
    public int y { get; }
    public Vector2 position { get; }

    public Node parent { get; set; }
    public Ball ball { get; private set; }

    public int targetDist { get; set; }
    public int startDist { get; set; }

    public int totalDist => targetDist + startDist;
    public Color ballColor => ball ? ball.color : Color.cyan;


    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
        position = new Vector2(x, y);
    }

    public int GetDistanceFrom(Node anotherNode)
    {
        int distX = Mathf.Abs(x - anotherNode.x);
        int distY = Mathf.Abs(y - anotherNode.y);

        return distX + distY;
    }

    public bool IsEmpty()
    {
        return ball == null;
    }

    public void AddBall(Ball ballObject)
    {
        ball = ballObject;
        ballObject.transform.position = position;
        ballObject.node = this;
    }

    public void RemoveBall()
    {
        ball.node = null;
        ball = null;
    }

}
