using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.instance.PlaySFX(4);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game!");
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Destroy(FindObjectOfType<GameSession>().gameObject);
        AudioManager.instance.PlaySFX(4);
    }
}
