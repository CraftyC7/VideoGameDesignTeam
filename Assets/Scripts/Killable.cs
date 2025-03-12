using Unity.VisualScripting;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public Vector3 originalPosition;
    public int deathParticles = 30;
    private Rigidbody2D _rb;
    private ParticleSystem _ps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        _ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Hazard") || collision.collider.CompareTag("DestructingHazard")) 
        {
            _ps.Emit(deathParticles);
            transform.position = originalPosition;
            _rb.linearVelocity = Vector2.zero;
        }
    }
}
