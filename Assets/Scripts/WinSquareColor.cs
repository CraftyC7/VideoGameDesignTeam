using UnityEngine;
using UnityEngine.UI;

public class WinSquareColor : MonoBehaviour
{
    private RawImage _ri;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ri = GetComponent<RawImage>();
        if (GameManager.playerOnePoints >= GameManager.pointsToWin)
        {
            _ri.color = new Color(0f, 1f, 1f);
        }
        else if (GameManager.playerTwoPoints >= GameManager.pointsToWin)
        {
            _ri.color = new Color(1f, 0f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
