using UnityEngine;

public class Explodeable : MonoBehaviour
{
    public int explosionParticles = 20;
    private ParticleSystem _ps;
    private SpriteRenderer _sr;
    private CircleCollider2D _cc;
    private bool _dead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        _sr = GetComponent<SpriteRenderer>();
        _cc = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_dead && !_ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent("Killable") != null && !_dead)
        {
            if (_ps != null)
            {
                _ps.Emit(explosionParticles);
            }
        }
        _sr.enabled = false;
        _cc.enabled = false;
        _dead = true;
    }
}
