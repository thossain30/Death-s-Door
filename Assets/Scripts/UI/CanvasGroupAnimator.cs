using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupAnimator : MonoBehaviour
{
    public AnimationCurve fadeCurve;
    public float fadeDuration;

    public CanvasGroup canvasGroup;

    private Coroutine fadeCoroutine;
    private System.Action fadeCallback;

    private void Awake()
    {
        canvasGroup.alpha = 0;
    }

    public void StartFadeIn(System.Action callback=null)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCallback?.Invoke();

        fadeCallback = callback;
        fadeCoroutine = StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float t = canvasGroup.alpha * fadeDuration;

        while (t < fadeDuration)
        {
            canvasGroup.alpha = fadeCurve.Evaluate(t / fadeDuration);

            t += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;
        fadeCoroutine = null;
        fadeCallback?.Invoke();
        fadeCallback = null;
    }

    public void StartFadeOut(System.Action callback = null)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCallback?.Invoke();

        fadeCallback = callback;
        fadeCoroutine = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float t = (1 - canvasGroup.alpha) * fadeDuration;

        while (t < fadeDuration)
        {
            canvasGroup.alpha = 1 - fadeCurve.Evaluate(t / fadeDuration);

            t += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        fadeCoroutine = null;
        fadeCallback?.Invoke();
        fadeCallback = null;
    }
}
