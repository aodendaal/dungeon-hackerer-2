using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [HideInInspector]
    public static GameInfo instance;

    public TMP_Text gameText;


    private Queue<string> messages = new Queue<string>();
    private const int maxMessages = 6;


    void Awake()
    {
        instance = this;
    }

    public void Log(string message)
    {
        messages.Enqueue(message);
        if (messages.Count > maxMessages)
        {
            messages.Dequeue();
        }

        var result = messages.Aggregate((a, n) => a + "\n" + n);

        gameText.SetText(result);
    }
}
