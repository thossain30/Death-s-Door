using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//controls opening canvas in each scene
public class CanvasText : MonoBehaviour
{
    public GameObject canvasPanel;
    public CanvasGroupAnimator fadeAnimator;
    private static bool isPaused;
    //Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    //opens up canvas and text!
    public void openCanvas()
    {
        isPaused = true;
        canvasPanel.SetActive(true);
        Time.timeScale = 0;

        fadeAnimator.StartFadeIn();
    }
    public void closeCanvas()
    {
        isPaused = false;
        Time.timeScale = 1;

        fadeAnimator.StartFadeOut(() =>
        {
            if (fadeAnimator.canvasGroup.alpha == 0) canvasPanel.SetActive(false);
        });
    }
    public static bool IsUIOpen()
    {
        return isPaused;
    }
    public void CompleteTrial()
    {
        SceneManager.LoadScene("Lobby");
    }
}
