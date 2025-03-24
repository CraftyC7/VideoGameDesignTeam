using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int[] levelSequence;
    public static int nextLevel = 1;
    public static int playerOnePoints = 0;
    public static int playerTwoPoints = 0;
    public static int pointsToWin = 3;
    public Image fade;
    public RawImage bluePoint;
    public RawImage redPoint;
    public float fadeLength = 1f;
    public int levelAmount = 5;
    public Color inactive;
    public Color blue;
    public Color red;
    private RawImage[] bluePoints = new RawImage[pointsToWin];
    private RawImage[] redPoints = new RawImage[pointsToWin];
    private bool isFading = false;
    public int[] registeredLevels;
    private int[] tempRegistered;

    private void FadeInScene(Scene scene, LoadSceneMode mode)
    {
        if (registeredLevels.Contains(scene.buildIndex))
        {
            for (int i = 0; i < bluePoints.Length; i++)
            {
                bluePoints[i].gameObject.SetActive(true);
                if (i < playerOnePoints)
                {
                    bluePoints[i].color = blue;
                }
                else
                {
                    bluePoints[i].color = inactive;
                }
            }

            for (int i = 0; i < redPoints.Length; i++)
            {
                redPoints[i].gameObject.SetActive(true);
                if (i < playerTwoPoints)
                {
                    redPoints[i].color = red;
                }
                else
                {
                    redPoints[i].color = inactive;
                }
            }
        }
        else
        {
            for (int i = 0; i < bluePoints.Length; i++)
            {
                bluePoints[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < redPoints.Length; i++)
            {
                redPoints[i].gameObject.SetActive(false);
            }
        }
        StartCoroutine(FadeIn());
    }

    public static void FadeScene(int sceneBuild)
    {
        if (!instance.isFading)
        {
            instance.StartCoroutine(instance.FadeOut(sceneBuild));
        }
    }

    private IEnumerator FadeIn()
    {
        float time = fadeLength;
        while (time > 0)
        {
            time -= Time.deltaTime;
            fade.color = new Color(0, 0, 0, time / fadeLength);
            yield return null;
        }
        fade.color = new Color(0, 0, 0, 0);
    }

    private IEnumerator FadeOut(int sceneBuild)
    {
        isFading = true;

        float time = 0;
        while (time < fadeLength)
        {
            time += Time.deltaTime;
            fade.color = new Color(0, 0, 0, time / fadeLength);
            yield return null;
        }
        fade.color = new Color(0, 0, 0, 1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneBuild);
        while (!operation.isDone)
        {
            yield return null;
        }

        isFading = false;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += FadeInScene;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < pointsToWin; i++)
        {
            Vector3 bluePos = bluePoint.transform.position;
            Vector3 redPos = redPoint.transform.position;
            RawImage newBlue = Instantiate(bluePoint, new Vector3(bluePos.x + 12 * i, bluePos.y, bluePos.z), new Quaternion());
            RawImage newRed = Instantiate(redPoint, new Vector3(redPos.x + 12 * i, redPos.y, redPos.z), new Quaternion());
            newBlue.transform.SetParent(bluePoint.transform.parent, true);
            newRed.transform.SetParent(redPoint.transform.parent, true);
            bluePoints[i] = newBlue;
            redPoints[i] = newRed;
        }
        Destroy(bluePoint.gameObject);
        Destroy(redPoint.gameObject);
    }

    private void GenerateLevelSequence()
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
        FadeScene(levelSequence[0]);
    }
}
