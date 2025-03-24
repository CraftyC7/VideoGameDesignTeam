using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.FadeScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
