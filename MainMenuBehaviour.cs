using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.ComponentModel;

struct LevelInfo
{

    public string levelName,sceneName;
    public Sprite previewImage;
    public GameObject extreInfoGroup;

    public LevelInfo(string x,string y, Sprite z,GameObject a)
    {
        levelName = x;
        sceneName = y;
        previewImage = z;
        extreInfoGroup = a;
}
}

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] List<string> names = new List<string>();
    [SerializeField] List<string> scenes = new List<string>();
    [SerializeField] List<Sprite> images = new List<Sprite>();
    [SerializeField] List<GameObject> extraInfoObjects = new List<GameObject>();
    [SerializeField] Image currentLevelImage;
    [SerializeField] TMP_Text currentLevelName;
    [SerializeField] GameObject previousButton, nextButton, loadingScreen, winSymbols;
    [SerializeField] Image loadingBarFill;
    [SerializeField] AudioSource fanfareNoise;

    private List<LevelInfo> levels = new List<LevelInfo>();
    private int currentLevel;

    void Start()
    {
        currentLevel = 0;
        previousButton.SetActive(false);

        for (int i = 0;i<names.Count;i++)
        {
            levels.Add(new LevelInfo(names[i], scenes[i], images[i], extraInfoObjects[i]));
        }

        currentLevelImage.sprite = levels[currentLevel].previewImage;
        currentLevelName.text = levels[currentLevel].levelName;

        SetWinSymbols();
    }

    public void GameEnd()
    {
        UnityEngine.Application.Quit();
    }

    public void GoToLevel()
    {
        if(levels[currentLevel].sceneName != "N/A")
        {
            ChangeScene(levels[currentLevel].sceneName);
        }
    }

    private void ChangeScene([SerializeField] string levelName)
    {
        StartCoroutine(LoadSceneAsync(levelName));
    }

    public void NextLevel()
    {
        currentLevel++;
        previousButton.SetActive(true);
        if (currentLevel == levels.Count - 1)
        {
            nextButton.SetActive(false);
        }
        currentLevelImage.sprite = levels[currentLevel].previewImage;
        currentLevelName.text = levels[currentLevel].levelName;
        SetWinSymbols();
    }

    public void PreviousLevel()
    {
        currentLevel--;
        nextButton.SetActive(true);
        if (currentLevel == 0)
        {
            previousButton.SetActive(false);
        }
        currentLevelImage.sprite = levels[currentLevel].previewImage;
        currentLevelName.text = levels[currentLevel].levelName;
        SetWinSymbols();
    }

    public void DeleteSave()
    {
        for(int i = 0; i < levels.Count; i++)
        {
            PlayerPrefs.DeleteKey(("LevelWin" + i.ToString()));
        }
        SetWinSymbols();
        PlayerPrefs.SetInt("RemovedCount", 0);
        PlayerPrefs.SetInt("MistakeCount",0);
        PlayerPrefs.SetInt("LossesCount", 0);
        this.gameObject.GetComponent<ExtraMenuBehaviour>().SetStatistics();
        fanfareNoise.Play();
    }

    private void SetWinSymbols()
    {
        winSymbols.SetActive(false);
        if (PlayerPrefs.HasKey(("LevelWin"+ currentLevel.ToString())))
        {
              winSymbols.SetActive(true);
        }
    }

    public void CurrentExtraInfoStateChange()
    {
        if (!levels[currentLevel].extreInfoGroup.activeSelf)
        {
            levels[currentLevel].extreInfoGroup.SetActive(true);
        }
        else
        {
            levels[currentLevel].extreInfoGroup.SetActive(false);
        }
    }

    IEnumerator LoadSceneAsync(string levelName)
    {
        UnityEngine.AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
    }
}
