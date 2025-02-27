using UnityEngine;
using System.Collections.Generic;

public class PathMovement : MonoBehaviour
{
    public List<List<float>> pathPonts = new List<List<float>>();
    public bool sameTime = false;
    public float speed = 0.2f;
    public float timeBetween = 1.0f;
    private int _currentPoint = 0;
    private float _timeOnPath = 0f;
    private float _distCurrentPath = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
