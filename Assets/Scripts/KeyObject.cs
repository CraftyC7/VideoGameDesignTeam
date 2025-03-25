using UnityEngine;

public class KeyObject : MonoBehaviour
{
    public Sprite downSprite;
    private AudioSource _as;
    private SpriteRenderer _sr;
    private bool _collected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _as = GetComponent<AudioSource>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Playable") && !_collected)
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
                GameManager.FadeScene(2);
            }

            GlobalDoor.doorOpen = true;
            _sr.sprite = downSprite;
            _as.Play();
            _collected = true;
        }
    }
}
