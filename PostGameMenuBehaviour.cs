using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PostGameMenuBehaviour : MonoBehaviour
{
    [SerializeField] TMP_Text exitReasonText = null;
    [SerializeField] TMP_Text displayText = null;
    [SerializeField] GameObject screenGroup;
    [SerializeField] AudioSource winNoise;

    void Awake()
    {
        CamereBehaviour.isPaused = false;
    }

    void Start()
    {
        if (!AnomalyBehaviour.gameStatus)
        {
            displayText.text = "You Lose";
            if (AnomalyBehaviour.exitReason != null)
            {
                exitReasonText.text = AnomalyBehaviour.exitReason;
            }
        }

        if (AnomalyBehaviour.gameStatus)
        {
            exitReasonText.gameObject.SetActive(false);
            displayText.text = "You Win\n Congratulations!";
            StartCoroutine(WaitForWin());
            PlayerPrefs.SetInt(("LevelWin" + AnomalyBehaviour.currentLevel.ToString()), 1);
        }

    }

    public IEnumerator WaitForWin()
    {
        screenGroup.SetActive(false);
        yield return new WaitForSeconds(10.0f);
        screenGroup.SetActive(true);
        winNoise.Play();

    }

    public void ChangeScene([SerializeField] string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
