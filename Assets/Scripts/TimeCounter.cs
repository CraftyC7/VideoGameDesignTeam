using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeCounter : MonoBehaviour
{
    private float time = 0;
    private TextMeshProUGUI tmp;
    private AudioSource _as;
    private bool playedBeep = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void ResetTime(Scene scene, LoadSceneMode mode)
    {
        time = 0;
        playedBeep = false;
    }
    


    void Start()
    {
        time = 0;
        SceneManager.sceneLoaded += ResetTime;
        _as = GetComponent<AudioSource>();
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 1 && !playedBeep)
        {
            playedBeep = true;
            _as.Play();
        }

        if (playedBeep)
        {
            tmp.SetText((Mathf.Round((time - 1) * 100f) / 100f).ToString());
        }
        else
        {
            tmp.SetText("0.00");
        }
    }
}
