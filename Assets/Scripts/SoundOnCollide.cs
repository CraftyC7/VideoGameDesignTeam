using UnityEngine;

public class SoundOnCollide : MonoBehaviour
{
    private AudioSource _as;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _as = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Playable"))
        {
            _as.Play();
        }
    }
}
