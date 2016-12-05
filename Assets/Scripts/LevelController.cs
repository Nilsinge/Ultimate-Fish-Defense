using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
       Application.Quit();
    }

    public Scene GetCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene;
    }
}
