using System;

[Serializable]
class ScoreData
{
    public string date;
    public int score;

    public ScoreData(string date, int score)
    {
        this.date = date;
        this.score = score;
    }
}
