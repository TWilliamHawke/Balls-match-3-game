using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsPool : Singleton<BallsPool>
{
    [SerializeField] Ball _ballPrefab;

    Queue<Ball> _ballsPool = new Queue<Ball>();

    public Ball GetBall()
    {
        if(_ballsPool.Count > 0)
        {
            var oldBall = _ballsPool.Dequeue();
            oldBall.gameObject.SetActive(true);
            return oldBall;
        }
        return Instantiate(_ballPrefab, Vector3.zero, transform.rotation);
    }

    public void HideBallFromNode(Node node)
    {
        Ball ball = node.ball;

        ball.gameObject.SetActive(false);
        node.RemoveBall();
        _ballsPool.Enqueue(ball);
    }
}
