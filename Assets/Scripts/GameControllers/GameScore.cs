static public class GameScore
{
    static public event System.Action<int> OnScoreChange;

    static public int score { get; private set; } = 0;
    static string CURRENT_SCORE_DATA { get; } = "current_score_data";


    static public void IncreaseScore(int num)
    {
        score += num;
        OnScoreChange?.Invoke(score);
    }

    static public void DecreaseScore(int num)
    {
        score -= num;
        OnScoreChange?.Invoke(score);
    }

    static public void ResetScore()
    {
        score = 0;
        SaveController.Clear(CURRENT_SCORE_DATA);
        OnScoreChange?.Invoke(score);
    }

    static public void SaveScore()
    {
        SaveController.Save<int>(CURRENT_SCORE_DATA, score);
        score = 0;
    }

    static public void LoadScore()
    {
        score = SaveController.Load<int>(CURRENT_SCORE_DATA);
        OnScoreChange?.Invoke(score);
    }

    static public bool HasSavedScore()
    {
        return SaveController.DataIsExist(CURRENT_SCORE_DATA);
    }


}
