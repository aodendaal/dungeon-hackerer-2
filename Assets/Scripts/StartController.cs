﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public void StartButton_Click()
    {
        SceneManager.LoadScene(1);
    }
}