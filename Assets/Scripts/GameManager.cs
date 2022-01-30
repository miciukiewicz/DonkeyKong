using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int Score;

    private void Start() {
        DontDestroyOnLoad(this);
    }
    public static void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log($"Current score: {Score}");
    }

    internal static void AddPoints() {
        Score += 1000;
    }
}
