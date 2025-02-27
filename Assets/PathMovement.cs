using UnityEngine;

public class PathMovement : MonoBehaviour
{
    public Vector2[] pathPoints;
    public bool sameTime = false;
    public float speed = 0.1f;
    public float timeBetween = 1.0f;
    private int _currentPoint = 0;
    private float _timeOnPath = 0f;
    private float _distCurrentPath = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (pathPoints.Length > 0)
        {
            Vector2 point = pathPoints[0];
            transform.position = new Vector3(point[0], point[1], transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (pathPoints.Length > 1)
        {
            if (sameTime)
            {
                
            }
            else
            {
                Vector2 curPoint = pathPoints[_currentPoint];
                float dist = Vector2.Distance(transform.position, curPoint);
                float angle = Mathf.Atan2(curPoint[1] - transform.position.y, curPoint[0] - transform.position.x);
                Vector3 currentPosition = transform.position;
                if (dist < speed)
                {
                    transform.position = new Vector3(curPoint[0], curPoint[1], currentPosition.z);
                    if (_currentPoint == pathPoints.Length - 1)
                    {
                        _currentPoint = 0;
                    }
                    else 
                    {
                        _currentPoint++;
                    }
                }
                else
                {
                    transform.position = new Vector3(currentPosition.x + Mathf.Cos(angle) * speed, currentPosition.y + Mathf.Sin(angle) * speed, currentPosition.z);
                }
            }
        }
    }
}
