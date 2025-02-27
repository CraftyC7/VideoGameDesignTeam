using Unity.VisualScripting;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public Vector3 originalPosition;
    private Rigidbody2D _rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.position;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Hazard")) 
        {
            transform.position = originalPosition;
            _rb.linearVelocity = Vector2.zero;
        }
    }
}
