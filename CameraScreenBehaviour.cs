using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraScreenBehaviour : MonoBehaviour
{
    [SerializeField] GameObject resultScreent, activeScreen, normalButtons,brokenButtons;
    [SerializeField] TMP_Text resultText, checkText;

    private bool canFadeIn = false;
    private float fadeTimer = 0;

    // Update is called once per frame
    void Update()
    {
        if(canFadeIn)
        {
            fadeTimer += Time.deltaTime;
            resultText.color = new Color(resultText.color.r, resultText.color.g, resultText.color.b, fadeTimer);
        }
    }

    public void CheckButton()
    {
        if (!GameObject.FindWithTag("GameController").GetComponent<AnomalyBehaviour>().IsChecking)
        {
            StartCoroutine(checkDisplayActions());
        }
    }

    public void ShowResult(bool result)
    {
        StartCoroutine(resultDisplayActions(result));
    }

    private IEnumerator resultDisplayActions(bool result)
    {
        activeScreen.SetActive(false);
        resultScreent.SetActive(true);
        if(result)
        {
            resultText.text = "REMOVED";
            resultText.color = Color.green;
            resultText.color = new Color(resultText.color.r, resultText.color.g, resultText.color.b, fadeTimer);
        }
        else
        {
            resultText.text = "NOT FOUND";
            resultText.color = Color.red;
            resultText.color = new Color(resultText.color.r, resultText.color.g, resultText.color.b, fadeTimer);
        }

        yield return new WaitForSeconds(1.0f);
        
        canFadeIn = true;
        
        yield return new WaitForSeconds(1.0f);

        canFadeIn = false;
        fadeTimer = 0;
        resultText.color = new Color(resultText.color.r, resultText.color.g, resultText.color.b, 0);
        activeScreen.SetActive(true);
        resultScreent.SetActive(false);
    }

    public void SwitchButtons()
    {
        if(normalButtons.activeSelf)
        {
            normalButtons.SetActive(false);
            brokenButtons.SetActive(true);
        }
        else
        {
            normalButtons.SetActive(true);
            brokenButtons.SetActive(false);
        }
    }

    private IEnumerator checkDisplayActions()
    {
        int loop = -1;

        while (loop<3)
        {
            loop++;
            checkText.text = "";
            for (int i = 0; i < 3; i++)
            {
                checkText.text = checkText.text + " .";
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(0.5f);
        }

        checkText.text = "Check";
    }
}
