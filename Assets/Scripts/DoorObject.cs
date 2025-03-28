using System.Net.NetworkInformation;
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
    private bool hasTransitioned = false;
    private SpriteRenderer _sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        if (transition && !hasTransitioned)
        {
            GameManager.nextLevel++;
            GameManager.FadeScene(nextScene);
            hasTransitioned = true;
        }

        if (GlobalDoor.doorOpen) 
        {
            _sr.sprite = openSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GlobalDoor.doorOpen && collision.gameObject.CompareTag("Playable") && !transition)
        {
            if (collision.gameObject.name == "Player1")
            {
                GameManager.playerOnePoints += 1;
            }
            else if (collision.gameObject.name == "Player2")
            {
                GameManager.playerTwoPoints += 1;
            }

            if ((GameManager.playerOnePoints >= GameManager.pointsToWin) || (GameManager.playerTwoPoints >= GameManager.pointsToWin))
            {
                nextScene = 2;
            }

            transition = true;
            GlobalDoor.doorOpen = false;
        }
    }
}
