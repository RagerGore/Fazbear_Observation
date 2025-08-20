using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level2AnomalyBehaviour : AnomalyBehaviour
{
    [SerializeField]
    GameObject BoxChargeIndicator;

    private int previousRoom = -1, puppetTimer = 100;
    public static bool puppetAgro = false;

    void Start()
    {
        spawnTimer = 0.0f;
        gameTimer = -30.0f;
        specialTimer = 0.0f;
        interval = 30.0f;
        specialInterval = 1.20f;
        specialReady = true;

        specialHourStart = 0;
    }

    override public void RoomProd()
    {
        int roomNumber, checker = 0;
        bool activate = false;

        while (1 == 1)
        {
            roomNumber = UnityEngine.Random.Range(0, rooms.Count);
            while (roomNumber == this.GetComponent<CamereBehaviour>().currentCamera)
            {
                roomNumber = UnityEngine.Random.Range(0, rooms.Count);
            }
            if (testSelector >= 0)
            {
                roomNumber = testSelector;
            }

            if (!rooms[roomNumber].GetComponent<RoomBehaviour>().anomalyActive)
            {
                rooms[roomNumber].GetComponent<RoomBehaviour>().ActivateAnomaly();
                activate = true;
            }

            if (activate)
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

        if (anomalyCounter >= maxAnomalies)
        {
            UpdateIntKey("LossesCount");
            SendToMenu("Too Many Anomalies", false);
        }

        GenerateInterval();
    }

    public override void SendToMenu(string reason, bool exitStatus)
    {
        gameStatus = exitStatus;
        exitReason = reason;
        currentLevel = 0;
        SceneManager.LoadScene("PostGameScreen");
    }

    public override IEnumerator CheckWait(int currentRoom)
    {
        timerStop = true;
        IsChecking = true;
        CamereBehaviour.canPause = false;

        yield return new WaitForSeconds(6.0f);

        switch (currentRoom)
        {
            case 0:
                break;
            default:
                break;
        }

        if (mistakeCounter > maxMistakes)
        {
            UpdateIntKey("LossesCount");
            SendToMenu("Too Many Mistakes", false);
        }

        yield return new WaitForSeconds(2.0f);

        CamereBehaviour.canPause = true;
        timerStop = false;
        IsChecking = false;
    }

    public override void SpecialAbility()
    {
        int roomNumber;

        specialLimiter = 0;

        if (!puppetAgro)
        {
            puppetTimer -= 1;
            if(puppetTimer <= 0)
            {
                puppetAgro = true;
            }

            BoxChargeIndicator.GetComponent<Slider>().value = puppetTimer;
        }
        else
        {
            specialInterval = UnityEngine.Random.Range(20, 40);
            roomNumber = UnityEngine.Random.Range(0, rooms.Count);
            rooms[roomNumber].GetComponent<RoomBehaviour>().ActivateSpecialAbility();
        }

        GenerateSpecialInterval();
    }

    public override void GenerateSpecialInterval()
    {
        specialTimer = 0.0f;

        if (nightTimeReference >= 3 || nightTimeReference == 4)
        {
            specialInterval = 1.0f;
        }
        if (nightTimeReference >= 4 || nightTimeReference == 5)
        {
            specialInterval = 0.8f;
        }
        if (nightTimeReference >= 5)
        {
            specialInterval = 0.6f;
        }

        if (puppetAgro)
        {
            specialInterval = UnityEngine.Random.Range(20, 40);
        }
    }

    public void AddToCounter() 
    {
        if(!puppetAgro && specialLimiter != 1)
        {
            puppetTimer += 5;
            if(puppetTimer > 100)
            {
                puppetTimer = 100;
            }
            BoxChargeIndicator.GetComponent<Slider>().value = puppetTimer;
            specialLimiter = 1;
        }
    }

    public void SetPreviousRoom(int room) { previousRoom = room; }
}
