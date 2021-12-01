using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //public static event System.EventHandler<System.EventArgs> CloseUI;

    public GameObject pausePanel;
    private static bool isPaused;

    public Slider VolumeSlider;

    public CanvasGroupAnimator fadeAnimator;

    // Start is called before the first frame update
    void Start()
    {
        UpdateOptionItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsUIOpen())
            {
                //InvokeCloseUI();
                ClosePauseMenu();
            }
            else
            {
                OpenPauseMenu();
            }
        }
    }

    public static bool IsUIOpen()
    {
        return isPaused;
    }


    // for pause menu

    public void OpenPauseMenu()
    {
        isPaused = true;

        pausePanel.SetActive(true);
        Time.timeScale = 0;

        fadeAnimator.StartFadeIn();
    }

    public void ClosePauseMenu()
    {
        isPaused = false;
        Debug.Log("clicked");

        //pausePanel.SetActive(false);
        Time.timeScale = 1;

        fadeAnimator.StartFadeOut(() =>
        {
            if (fadeAnimator.canvasGroup.alpha == 0) pausePanel.SetActive(false);
        });
    }

    public void ChangeScene(string sceneName)
    {
        Debug.Log("Loading scene " + sceneName + "...");
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApplication()
    {
        Debug.Log("Quitting application...");
        Application.Quit();
    }

    public void UpdateOptionItems()
    {
        //VolumeSlider.value = AudioManager.GetSFXVolume();
    }
    public void PlayGame()
    {
        GroundTile.complete = false;
        mazefloor.complete = false;
        T3ButtonPuzzleManager.complete = false;
        isPaused = false;
        SceneManager.LoadScene("Lobby");
    }
}
