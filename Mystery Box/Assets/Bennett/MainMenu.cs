using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject mainUI;
    public GameObject creditUI;
    AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        Debug.Log("started");
    }

    public void Play()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void Credits()
    {
        mainUI.SetActive(false);
        creditUI.SetActive(true);
    }

    public void Back()
    {
        creditUI.SetActive(false);
        mainUI.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
