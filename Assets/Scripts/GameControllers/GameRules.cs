using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameRules: Singleton<GameRules>
{
    [SerializeField] List<Color> colorList = new List<Color>();
    [SerializeField] int _startBallCount = 5;
    [SerializeField] List<int> _scorePerBalls = new List<int>();

    public int startBallsCount => _startBallCount;


    public Color GetRandomColor()
    {
        if(colorList.Count == 0)
        {
            return Color.black;
        }
        int idx = Random.Range(0, colorList.Count);
        return colorList[idx];
    }

    public int GetScore(int ballsCount)
    {

        int awardLevel = ballsCount - 5;
        if(awardLevel < 0)
        {
            return 0;
        }
        else if (awardLevel >= _scorePerBalls.Count )
        {
            return _scorePerBalls.Last();
        }
        else
        {
            return _scorePerBalls[awardLevel];
        }
    }

    public Color GetColor(int idx)
    {
        if (colorList[idx] != null)
        {
            return colorList[idx];
        }
        else
        {
            return Color.black;
        }
    }

    public int GetColorIdx(Color color)
    {
        if (colorList.Contains(color))
        {
            return colorList.IndexOf(color);
        }
        else
        {
            return -1;
        }
    }
}
