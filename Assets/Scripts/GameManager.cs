using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int[] levelSequence;
    public static int nextLevel = 1;
    public static int playerOnePoints = 0;
    public static int playerTwoPoints = 0;
    public static int pointsToWin = 3;
    public int levelAmount = 5;
    public int[] registeredLevels;
    private int[] tempRegistered;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void GenerateLevelSequence()
    {
        levelSequence = new int[levelAmount];
        tempRegistered = new int[registeredLevels.Length];
        Array.Copy(registeredLevels, tempRegistered, registeredLevels.Length);
        for (int i = 0; i < levelSequence.Length; i++) 
        {
            int rand = UnityEngine.Random.Range(0, tempRegistered.Length);
            while (tempRegistered[rand] == -1)
            {
                rand = UnityEngine.Random.Range(0, tempRegistered.Length);
            }

            levelSequence[i] = tempRegistered[rand];
            tempRegistered[rand] = -1;
        }
    }

    public void StartGame()
    {
        GenerateLevelSequence();
        nextLevel = 1;
        playerOnePoints = 0;
        playerTwoPoints = 0;
        SceneManager.LoadScene(levelSequence[0]);
    }
}
