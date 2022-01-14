using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    Collider2D levelExitCollider;

    void Start()
    {
        levelExitCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (levelExitCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(1);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
