using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField] List<ScoreRow> _scoreTable;
    [SerializeField] Color _selectedRowColor;
    [SerializeField] YourScore _yourScore;

    const string SCORE_DATA ="score_data";

    public Color rowColor => _selectedRowColor;

    List<ScoreData> _scoreList;

    void Start()
    {
        _scoreList = SaveController.Load<List<ScoreData>>(SCORE_DATA);

        if (_scoreList == null || _scoreList.Count == 0)
        {
            GetDefaultScoreList();
        }

        if(SceneLoader.levelState == LevelState.scoreScreen)
        {
            _yourScore.SetText(GameScore.score);
            UpdateScoreList();
        }
        else
        {
            _yourScore.Hide();
        }

        for (int i = 0; i < _scoreList.Count; i++)
        {
            _scoreTable[i].SetText(_scoreList[i].date, _scoreList[i].score);
        }
    }

    private void UpdateScoreList()
    {
        int score = GameScore.score;
        GameScore.ResetScore();
        var newScoreList = new ScoreData[_scoreList.Count];
        int index = 0;
        for (int i = 0; i < _scoreList.Count; i++)
        {
            if (score >= _scoreList[index].score)
            {
                newScoreList[i] = new ScoreData(DateTime.Now.ToString("d"), score);
                score = -1;
                _scoreTable[i].Select();
                continue;
            }
            else
            {
                newScoreList[i] = _scoreList[index];
                index++;
            }
        }
        _scoreList.Clear();
        _scoreList.AddRange(newScoreList);
        SaveController.Save<List<ScoreData>>(SCORE_DATA, _scoreList);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Cancel") != 0)
        {
            Hide();
        }

    }

    void GetDefaultScoreList()
    {
        _scoreList = new List<ScoreData>()
        {
            new ScoreData("Long ago", 250),
            new ScoreData("Long ago", 200),
            new ScoreData("Long ago", 150),
            new ScoreData("Long ago", 100),
            new ScoreData("Long ago", 50)
        };
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (SceneLoader.levelState == LevelState.mainMenu)
        {
            gameObject.SetActive(false);
        }
        else
        {
            SceneLoader.LoadMainMenu();
        }
    }
}
