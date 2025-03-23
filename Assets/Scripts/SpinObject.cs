using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public float speed = 10f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, 10);
    }
}
