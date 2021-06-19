using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreRow : MonoBehaviour
{

    [SerializeField] Transform _rowSelector;
    [SerializeField] Text _DateText;
    [SerializeField] Text _ScoreText;

    ScoreScreen _scoreScreen;

    void Awake()
    {
        _scoreScreen = FindObjectOfType<ScoreScreen>();
    }

    public void Select()
    {
        var image = GetComponent<Image>();
        image.color = _scoreScreen.rowColor;
        _rowSelector.gameObject.SetActive(true);
    }

    public void SetText(string date, int score)
    {
        _DateText.text = date;
        _ScoreText.text = score.ToString();
    }
}
