using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_T3Display : MonoBehaviour
{
    public Image image;
    private Color origColor;

    private void Awake()
    {
        origColor = image.color;
    }

    public void SetColor(Color c)
    {
        image.color = c;
    }

    public void Ping(Color c, float duration, AnimationCurve intensity)
    {
        StartCoroutine(AnimatePing(c, duration, intensity));
    }

    private IEnumerator AnimatePing(Color c, float duration, AnimationCurve intensity)
    {
        float t = 0;

        while (t < duration)
        {
            float v = intensity.Evaluate(t / duration);
            SetColor((c * v) + (origColor * (1 - v)));

            t += Time.deltaTime;
            yield return null;
        }

        ResetColor();
    }

    public void ResetColor()
    {
        StopAllCoroutines();
        image.color = origColor;
    }
}
