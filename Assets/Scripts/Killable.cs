using Unity.VisualScripting;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public Vector3 originalPosition;
    public int deathParticles = 30;
    public AudioClip deathSound;
    private Rigidbody2D _rb;
    private ParticleSystem _ps;
    private SpriteRenderer _sr;
    private AudioSource _as;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        _ps = GetComponent<ParticleSystem>();
        _sr = GetComponent<SpriteRenderer>();
        _as = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Hazard") || collision.collider.CompareTag("DestructingHazard")) 
        {
            _ps.startColor = _sr.color;
            _ps.Emit(deathParticles);
            _as.clip = deathSound;
            _as.Play();
            transform.position = originalPosition;
            _rb.linearVelocity = Vector2.zero;
        }
    }
}
