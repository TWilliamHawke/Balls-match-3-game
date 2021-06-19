using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScoreCommand : Icommand
{
    int _score;

    public AddScoreCommand(int score)
    {
        _score = score;
    }

    public void Execute()
    {
        GameScore.IncreaseScore(_score);
    }

    public void Undo()
    {
        GameScore.DecreaseScore(_score);
    }
}
