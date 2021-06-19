using UnityEngine;
using UnityEngine.SceneManagement;

class SceneLoader : MonoBehaviour
{
    public static LevelState levelState { get; private set; } = LevelState.mainMenu;
    public static LevelLoader instance;


    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        levelState = LevelState.mainMenu;
    }

    public static void LoadScoreScreen()
    {
        SceneManager.LoadScene(1);
        levelState = LevelState.scoreScreen;
    }

    public static void StartNewGame()
    {
        SceneManager.LoadScene(2);
        levelState = LevelState.newGame;
    }

    public static void LoadGame()
    {
        SceneManager.LoadScene(2);
        levelState = LevelState.loadedGame;
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

}
