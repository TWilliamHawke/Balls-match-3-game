using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] Text _textField;

    // Start is called before the first frame update
    void Awake()
    {
        GameScore.OnScoreChange += UpdateScoreDisplay;
        UpdateScoreDisplay(0);
    }

    private void OnDestroy() {
        GameScore.OnScoreChange -= UpdateScoreDisplay;
    }

    void UpdateScoreDisplay(int score)
    {
        _textField.text = score.ToString();
    }
}
