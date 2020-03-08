using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    void Start()
    {
        AnalyticsResult result = Analytics.CustomEvent("Test");
        // This should print "Ok" if the event was sent correctly.
        Debug.Log(result);

        AnalyticsEvent.GameStart();
    }

    public void StartButton_Click()
    {
        GameController.IsPaused = false;
        GameController.level = 0;

        SceneManager.LoadScene(1);
    }
}
