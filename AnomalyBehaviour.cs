using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AnomalyBehaviour : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;
    [SerializeField] protected int testSelector = -1;

    static public GameObject StageFreddy, StageBonnie, StageChica, StageFoxy;
    static public bool StageFreddyEnabled = true, StageBonnieEnabled = true, StageChicaEnabled = true, StageFoxyEnabled = true;
    static public bool gameStatus = false;
    static public string exitReason = "N/A";
    static public int currentLevel;

    public List<GameObject> rooms = new List<GameObject>();
    public List<GameObject> mistakeSymbols = new List<GameObject>();

    public GameObject activeCamera;

    // 12 minute night will last 720 seconds. With that in mind, 6 hours (12 PM to 6 AM) needs each hour to be 120 seconds

    public float spawnTimer = 0.0f;
    public float gameTimer = -30.0f;
    public float specialTimer = 0.0f;
    public float interval = 30.0f;
    public float specialInterval = 0.0f;

    public int anomalyCounter = 0;
    public int mistakeCounter = 0;

    public bool IsChecking = false;

    protected bool timerStop = false;
    protected int nightTimeReference = 0;

    protected int maxAnomalies = 5; //Maximum 4 anomalies should exist at the same time. On the 5th, the game ends | Once the night is past 5AM, two anomalies will spawn at once and the max will go up by 1

    protected int maxMistakes = 3;

    protected bool specialReady = false;
    protected int specialLimiter, specialCounter = 0;
    protected int specialHourStart;

    void Update()
    {
        gameTimer += Time.deltaTime;

        if (!timerStop)
        {
            spawnTimer += Time.deltaTime;
            specialTimer += Time.deltaTime;
        }

        if(spawnTimer >= interval)
        {
            RoomProd();
        }

        CheckForSpecial();
        if ((specialTimer >= specialInterval) && (specialReady))
        {
            SpecialAbility();
        }

        if(gameTimer >= 120.0f)
        {
            //Update Time
            gameTimer = 0.0f;
            nightTimeReference++;
            timeText.text = nightTimeReference.ToString() + " AM";
            if(nightTimeReference >= 6)
            {
                SendToMenu("N/A", true);
            }
        }

        if(Input.GetKey("c") && Input.GetKey("d") && Input.GetKey("="))
        {
            SendToMenu("N/A", true);
        }
    }

    virtual public void RoomProd()
    {
        int roomNumber,  checker = 0;
        bool activate = false;

        while(1==1)
        {
            roomNumber = UnityEngine.Random.Range(0, rooms.Count);
            while (roomNumber == this.GetComponent<CamereBehaviour>().currentCamera)
            {
                roomNumber = UnityEngine.Random.Range(0, rooms.Count);
            }

            switch (roomNumber)
            {
                case 0:
                    if (!rooms[roomNumber].GetComponent<Room1Behaviour>().anomalyActive)
                    {
                        rooms[roomNumber].GetComponent<Room1Behaviour>().ActivateAnomaly();
                        activate = true;
                        break;
                    }
                    break;
                case 1:
                    if (!rooms[roomNumber].GetComponent<Room2Behaviour>().anomalyActive)
                    {
                        rooms[roomNumber].GetComponent<Room2Behaviour>().ActivateAnomaly();
                        activate = true;
                        break;
                    }
                    break;
                default:
                    break;
            }
            if(activate)
            {
                anomalyCounter++;
                break;
            }

            if (!activate)
            {
                checker++;
                if (checker > 50)
                {
                    break;
                }
            }
        }

        if(anomalyCounter >= maxAnomalies)
        {
            SendToMenu("Too Many Anomalies", false);
        }

        GenerateInterval();
    }

    public void AnomalyCheck(int currentRoom)
    {
        StartCoroutine(CheckWait(currentRoom));
    }

    public virtual void SendToMenu (string reason, bool exitStatus)
    {
        gameStatus = exitStatus;
        exitReason = reason;
        currentLevel = -1;
        SceneManager.LoadScene("PostGameScreen");
    }

    public virtual IEnumerator CheckWait(int currentRoom)
    {
        timerStop = true;
        IsChecking = true;
        CamereBehaviour.canPause = false;

        yield return new WaitForSeconds(6.0f);

        switch (currentRoom)
        {
            case 0:
                if (rooms[currentRoom].GetComponent<Room1Behaviour>().anomalyActive)
                {
                    rooms[currentRoom].GetComponent<Room1Behaviour>().DeactivateAnomaly();
                    anomalyCounter--;
                    activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(true);
                }
                else
                {
                    mistakeCounter++;
                    if (mistakeCounter < 4)
                    {
                        activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(false);
                    }
                    if (mistakeCounter - 1 < mistakeSymbols.Count)
                    {
                        mistakeSymbols[(mistakeCounter - 1)].SetActive(true);
                    }
                }
                break;
            case 1:
                if (rooms[currentRoom].GetComponent<Room2Behaviour>().anomalyActive)
                {
                    rooms[currentRoom].GetComponent<Room2Behaviour>().DeactivateAnomaly();
                    anomalyCounter--;
                    activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(true);
                }
                else
                {
                    mistakeCounter++;
                    if (mistakeCounter < 4)
                    {
                        activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(false);
                    }
                    if (mistakeCounter-1 < mistakeSymbols.Count)
                    {
                        mistakeSymbols[(mistakeCounter - 1)].SetActive(true);
                    }
                }
                break;
            default:
                break;
        }

        if (mistakeCounter > maxMistakes)
        {
            SendToMenu("Too Many Mistakes", false);
        }

        yield return new WaitForSeconds(4.0f);

        CamereBehaviour.canPause = true;
        timerStop = false;
        IsChecking = false;
    }

    protected void GenerateInterval()
    {
        spawnTimer = 0.0f;

        if (nightTimeReference == specialHourStart)
        {
            specialReady = true;
        }

        if (nightTimeReference >= 0 || nightTimeReference <= 2)
        {
            interval = UnityEngine.Random.Range(20.0f, 45.0f);
        }
        if (nightTimeReference == 3 || nightTimeReference == 4)
        {
            interval = UnityEngine.Random.Range(20.0f, 35.0f);
        }
        if (nightTimeReference >= 5 )
        {
            maxAnomalies = 6;
            interval = UnityEngine.Random.Range(15.0f, 40.0f);
        }
    }

    protected void UpdateIntKey(string keyName)
    {
        if(!PlayerPrefs.HasKey(keyName))
        {
            PlayerPrefs.SetInt(keyName, 0);
        }

        PlayerPrefs.SetInt(keyName, (PlayerPrefs.GetInt(keyName)+1));
    }

    public virtual void SpecialAbility() { }
    public virtual void CheckForSpecial() { }
    public virtual void GenerateSpecialInterval() { }

    public void IncreaseSpecialCounter(){ specialCounter++; }
    public void ResetSpecialCounter() { specialCounter = 0; }
    public void SetSpecialReady(bool state){ specialReady = state; }
}
