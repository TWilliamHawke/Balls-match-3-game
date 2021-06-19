using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextBalls : MonoBehaviour
{
    [SerializeField] Image[] _nextBalls;

    void Awake()
    {
        BallSpawner.OnUpdateBalls += UpdateBallsColor;
    }

    private void OnDestroy() {
        BallSpawner.OnUpdateBalls -= UpdateBallsColor;
    }

    void UpdateBallsColor(Color[] colors)
    {
        for(int i = 0; i< colors.Length; i++)
        {
            _nextBalls[i].color = colors[i];
        }
    }
}
