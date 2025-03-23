using UnityEngine;
using UnityEngine.SceneManagement;

public static class GlobalDoor
{
    public static bool doorOpen = false;
}

public class DoorObject : MonoBehaviour
{
    public int nextScene = 0;
    public Sprite openSprite;
    private bool transition = false;
    private AudioSource _as;
    private SpriteRenderer _sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _as = GetComponent<AudioSource>();
        _sr = GetComponent<SpriteRenderer>();
        if (GameManager.nextLevel >= GameManager.levelSequence.Length)
        {
            nextScene = 2;
        }
        else
        {
            nextScene = GameManager.levelSequence[GameManager.nextLevel];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transition)
        {
            GameManager.nextLevel++;
            SceneManager.LoadScene(nextScene);
        }

        if (GlobalDoor.doorOpen) 
        {
            _sr.sprite = openSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GlobalDoor.doorOpen && collision.gameObject.CompareTag("Playable"))
        {
            if (collision.gameObject.name == "Player1")
            {
                GameManager.playerOnePoints += 1;
            }
            else if (collision.gameObject.name == "Player2")
            {
                GameManager.playerTwoPoints += 1;
            }

            if (GameManager.playerOnePoints >= GameManager.pointsToWin || GameManager.playerTwoPoints >= GameManager.pointsToWin)
            {
                nextScene = 2;
            }

            Debug.Log("P1: " + GameManager.playerOnePoints);
            Debug.Log("P2: " + GameManager.playerTwoPoints);

            _as.Play();
            transition = true;
            GlobalDoor.doorOpen = false;
        }
    }
}
