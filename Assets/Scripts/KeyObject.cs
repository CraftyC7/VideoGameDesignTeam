using UnityEngine;

public class KeyObject : MonoBehaviour
{
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
        if (_collected && !_as.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Playable") && !_collected)
        {
            GlobalDoor.doorOpen = true;
            _as.Play();
            _sr.enabled = false;
            _collected = true;
        }
    }
}
