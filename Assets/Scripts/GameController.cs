using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public static GameController instance;

    [HideInInspector]
    public static bool IsPaused = false;

    [HideInInspector]
    public static int level = 0;

    [Header("Panels")]
    public GameObject gamePanel;
    public GameObject pausePanel;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        try
        {
            level++;
            AnalyticsEvent.LevelStart(level);
            GameInfo.instance.Log($"Teleported to Level {level}");

            gamePanel.SetActive(true);
            pausePanel.SetActive(false);

            LevelController.instance.Initialize();
            MonsterController.instance.Initialize();

            var player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<SimpleMove>().PlacePlayer();
            player.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
        }
        catch (Exception ex)
        {
            GameInfo.instance.Log(ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void QuitButton_Click()
    {
        AnalyticsEvent.GameOver();

#if UNITY_WEBGL
        SceneManager.LoadScene(0);
#else
        Application.Quit();
#endif
    }

    public void ResumeButton_Click()
    {
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);

        IsPaused = false;
    }

    private void Pause()
    {
        if (!IsPaused)
        {
            //gamePanel.SetActive(false);
            pausePanel.SetActive(true);

            IsPaused = true;
        }
        else
        {
            ///gamePanel.SetActive(true);
            pausePanel.SetActive(false);

            IsPaused = false;
        }
    }
}
