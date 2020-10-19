using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;

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
        Destroy(gameObject);
        StartCoroutine(LoadSceneAfterSecAndDestroy(0, 3f));
    }

    private void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadSceneAfterSec(currentSceneIndex, 2.5f));
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
