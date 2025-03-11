using UnityEngine;
using UnityEngine.SceneManagement;

public static class GlobalDoor
{
    public static bool doorOpen = false;
}

public class DoorObject : MonoBehaviour
{
    public int nextScene = 0;
    private bool transition = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transition)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GlobalDoor.doorOpen && collision.gameObject.CompareTag("Playable"))
        {
            transition = true;
        }
    }
}
