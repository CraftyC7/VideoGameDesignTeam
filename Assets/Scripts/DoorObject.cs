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
    private AudioSource _as;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _as = GetComponent<AudioSource>();
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
            _as.Play();
            transition = true;
            GlobalDoor.doorOpen = false;
        }
    }
}
