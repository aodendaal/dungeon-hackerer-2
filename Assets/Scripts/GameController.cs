using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public static bool IsPaused = false;

    [Header("Panels")]
    public GameObject gamePanel;
    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);

        LevelController.instance.Initialize();
        MonsterController.instance.Initialize();

        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<SimpleMove>().PlacePlayer();
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
        Application.Quit();
    }

    private void Pause()
    {
        if (!IsPaused)
        {
            gamePanel.SetActive(false);
            pausePanel.SetActive(true);

            IsPaused = true;
        }
        else
        {
            gamePanel.SetActive(true);
            pausePanel.SetActive(false);

            IsPaused = false;
        }
    }
}
