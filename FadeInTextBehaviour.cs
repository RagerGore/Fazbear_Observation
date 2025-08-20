using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeInTextBehaviour : MonoBehaviour
{
    [SerializeField] TMP_Text fadeInText, flashText;

    private float fadeTimer = 0, flashTimer = 0;
    private bool flashDirection = true, canFlash = false;

    void Start()
    {
        fadeInText.color = new Color(fadeInText.color.r, fadeInText.color.g, fadeInText.color.b, 0);
        flashText.color = new Color(flashText.color.r, flashText.color.g, flashText.color.b, 0);
        StartCoroutine(waitToFlash());
    }

    void Update()
    {
        if (fadeInText != null)
        {
            fadeTimer += Time.deltaTime;
            fadeInText.color = new Color(fadeInText.color.r, fadeInText.color.g, fadeInText.color.b, fadeTimer);
        }

        if(flashText != null && canFlash)
        {
            if(flashDirection)
            {
                flashTimer += Time.deltaTime;
                flashText.color = new Color(flashText.color.r, flashText.color.g, flashText.color.b, flashTimer);
                if(flashTimer >= 1)
                {
                    flashDirection = false;
                }
            }
            if (!flashDirection)
            {
                flashTimer -= Time.deltaTime;
                flashText.color = new Color(flashText.color.r, flashText.color.g, flashText.color.b, flashTimer);
                if (flashTimer <= 0)
                {
                    flashDirection = true;
                }
            }
        }
    }

    private IEnumerator waitToFlash()
    {
        yield return new WaitForSeconds(3.0f);
        canFlash = true;
    }
}
