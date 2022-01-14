using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    public int score = 0;

    [SerializeField] Text livesText = null;
    [SerializeField] Text scoreText = null;


    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void AddToScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }

    public void DeathProcess()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void ResetGameSession()
    {
        StartCoroutine(LoadSceneAfterSecAndDestroy(0, 3f));
    }

    private void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadSceneAfterSec(currentSceneIndex, 2.5f));
        livesText.text = playerLives.ToString();
    }

    IEnumerator LoadSceneAfterSec(int sceneIndex, float timeSeconds)
    {
        yield return new WaitForSecondsRealtime(timeSeconds);
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator LoadSceneAfterSecAndDestroy(int sceneIndex, float timeSeconds)
    {
        yield return new WaitForSecondsRealtime(timeSeconds);
        SceneManager.LoadScene(sceneIndex);
        Destroy(gameObject);
    }
}
