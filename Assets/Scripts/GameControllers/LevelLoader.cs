using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{

    public static LevelState state { get; private set; } = LevelState.mainMenu;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        state = LevelState.mainMenu;
    }

    public void LoadScoreScreen()
    {
        SceneManager.LoadScene(1);
        state = LevelState.mainMenu;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(2);
        state = LevelState.newGame;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(2);
        state = LevelState.loadedGame;
    }

}
