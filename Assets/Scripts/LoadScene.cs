using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per framej
    public void LoadGame()
    {
        // Start game here
    }
}
