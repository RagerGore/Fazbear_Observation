using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1AnomalyBehaviour : AnomalyBehaviour
{
    private int previousRoom = -1;

    void Start()
    {
        spawnTimer = 0.0f;
        gameTimer = -30.0f;
        specialTimer = 0.0f;
        interval = 30.0f;
        specialInterval = 0.0f;

        specialHourStart = 2;
        specialLimiter = UnityEngine.Random.Range(2, 5);
    }

    override public void RoomProd()
    {
        int roomNumber, checker = 0;
        bool activate = false;

        while (1==1)
        {
            roomNumber = UnityEngine.Random.Range(0, rooms.Count);
            while (roomNumber == this.GetComponent<CamereBehaviour>().currentCamera)
            {
                roomNumber = UnityEngine.Random.Range(0, rooms.Count);
            }
            if(testSelector >= 0)
            {
                roomNumber = testSelector;
            }

            switch (roomNumber)
            {
                case 0:
                    if (!rooms[roomNumber].GetComponent<ShowStageBehaviour>().anomalyActive)
                    {
                        rooms[roomNumber].GetComponent<ShowStageBehaviour>().ActivateAnomaly();
                        activate = true;
                        break;
                    }
                    break;
                case 1:
                    if (!rooms[roomNumber].GetComponent<DiningHallBehaviour>().anomalyActive)
                    {
                        rooms[roomNumber].GetComponent<DiningHallBehaviour>().ActivateAnomaly();
                        activate = true;
                        break;
                    }
                    break;
                case 2:
                    if (!rooms[roomNumber].GetComponent<BackRoomBehaviour>().anomalyActive)
                    {
                        rooms[roomNumber].GetComponent<BackRoomBehaviour>().ActivateAnomaly();
                        activate = true;
                        break;
                    }
                    break;
                case 3:
                    if (!rooms[roomNumber].GetComponent<BathroomsBehaviour>().anomalyActive)
                    {
                        rooms[roomNumber].GetComponent<BathroomsBehaviour>().ActivateAnomaly();
                        activate = true;
                        break;
                    }
                    break;
                case 4:
                    if (!rooms[roomNumber].GetComponent<ClosetBehaviour>().anomalyActive)
                    {
                        rooms[roomNumber].GetComponent<ClosetBehaviour>().ActivateAnomaly();
                        activate = true;
                        break;
                    }
                    break;
                case 5:
                    if (!rooms[roomNumber].GetComponent<PirateCoveBehaviour>().anomalyActive)
                    {
                        rooms[roomNumber].GetComponent<PirateCoveBehaviour>().ActivateAnomaly();
                        activate = true;
                        break;
                    }
                    break;
                case 6:
                    if (!rooms[roomNumber].GetComponent<RightHallwayBehaviour>().anomalyActive)
                    {
                        rooms[roomNumber].GetComponent<RightHallwayBehaviour>().ActivateAnomaly();
                        activate = true;
                        break;
                    }
                    break;
                case 7:
                    if (!rooms[roomNumber].GetComponent<OfficeBehaviour>().anomalyActive)
                    {
                        rooms[roomNumber].GetComponent<OfficeBehaviour>().ActivateAnomaly();
                        activate = true;
                        break;
                    }
                    break;
                default:
                    break;
            }
            if (activate)
            {
                anomalyCounter++;
                break;
            }

            if (!activate)
            {
                checker++;
                if (checker>50)
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
                if (rooms[currentRoom].GetComponent<ShowStageBehaviour>().anomalyActive)
                {
                    rooms[currentRoom].GetComponent<ShowStageBehaviour>().DeactivateAnomaly();
                    anomalyCounter--;
                    activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(true);
                    UpdateIntKey("RemovedCount");
                }
                else
                {
                    mistakeCounter++;
                    UpdateIntKey("MistakeCount");
                    if (mistakeCounter < 4)
                    {
                        activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(false);
                    }
                    if (mistakeCounter - 1 < mistakeSymbols.Count)
                    {
                        mistakeSymbols[(mistakeCounter - 1)].SetActive(false);
                    }
                }
                break;
            case 1:
                if (rooms[currentRoom].GetComponent<DiningHallBehaviour>().anomalyActive)
                {
                    rooms[currentRoom].GetComponent<DiningHallBehaviour>().DeactivateAnomaly();
                    anomalyCounter--;
                    activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(true);
                    UpdateIntKey("RemovedCount");
                }
                else
                {
                    mistakeCounter++;
                    UpdateIntKey("MistakeCount");
                    if (mistakeCounter < 4)
                    {
                        activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(false);
                    }
                    if (mistakeCounter - 1 < mistakeSymbols.Count)
                    {
                        mistakeSymbols[(mistakeCounter - 1)].SetActive(false);
                    }
                }
                break;
            case 2:
                if (rooms[currentRoom].GetComponent<BackRoomBehaviour>().anomalyActive)
                {
                    rooms[currentRoom].GetComponent<BackRoomBehaviour>().DeactivateAnomaly();
                    anomalyCounter--;
                    activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(true);
                    UpdateIntKey("RemovedCount");
                }
                else
                {
                    mistakeCounter++;
                    UpdateIntKey("MistakeCount");
                    if (mistakeCounter < 4)
                    {
                        activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(false);
                    }
                    if (mistakeCounter - 1 < mistakeSymbols.Count)
                    {
                        mistakeSymbols[(mistakeCounter - 1)].SetActive(false);
                    }
                }
                break;
            case 3:
                if (rooms[currentRoom].GetComponent<BathroomsBehaviour>().anomalyActive)
                {
                    rooms[currentRoom].GetComponent<BathroomsBehaviour>().DeactivateAnomaly();
                    anomalyCounter--;
                    activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(true);
                    UpdateIntKey("RemovedCount");
                }
                else
                {
                    mistakeCounter++;
                    UpdateIntKey("MistakeCount");
                    if (mistakeCounter < 4)
                    {
                        activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(false);
                    }
                    if (mistakeCounter - 1 < mistakeSymbols.Count)
                    {
                        mistakeSymbols[(mistakeCounter - 1)].SetActive(false);
                    }
                }
                break;
            case 4:
                if (rooms[currentRoom].GetComponent<ClosetBehaviour>().anomalyActive)
                {
                    rooms[currentRoom].GetComponent<ClosetBehaviour>().DeactivateAnomaly();
                    anomalyCounter--;
                    activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(true);
                    UpdateIntKey("RemovedCount");
                }
                else
                {
                    mistakeCounter++;
                    UpdateIntKey("MistakeCount");
                    if (mistakeCounter < 4)
                    {
                        activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(false);
                    }
                    if (mistakeCounter - 1 < mistakeSymbols.Count)
                    {
                        mistakeSymbols[(mistakeCounter - 1)].SetActive(false);
                    }
                }
                break;
            case 5:
                if (rooms[currentRoom].GetComponent<PirateCoveBehaviour>().anomalyActive)
                {
                    rooms[currentRoom].GetComponent<PirateCoveBehaviour>().DeactivateAnomaly();
                    anomalyCounter--;
                    activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(true);
                    UpdateIntKey("RemovedCount");
                }
                else
                {
                    mistakeCounter++;
                    UpdateIntKey("MistakeCount");
                    if (mistakeCounter < 4)
                    {
                        activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(false);
                    }
                    if (mistakeCounter - 1 < mistakeSymbols.Count)
                    {
                        mistakeSymbols[(mistakeCounter - 1)].SetActive(false);
                    }
                }
                break;
            case 6:
                if (rooms[currentRoom].GetComponent<RightHallwayBehaviour>().anomalyActive)
                {
                    rooms[currentRoom].GetComponent<RightHallwayBehaviour>().DeactivateAnomaly();
                    anomalyCounter--;
                    activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(true);
                    UpdateIntKey("RemovedCount");
                }
                else
                {
                    mistakeCounter++;
                    UpdateIntKey("MistakeCount");
                    if (mistakeCounter < 4)
                    {
                        activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(false);
                    }
                    if (mistakeCounter - 1 < mistakeSymbols.Count)
                    {
                        mistakeSymbols[(mistakeCounter - 1)].SetActive(false);
                    }
                }
                break;
            case 7:
                if (rooms[currentRoom].GetComponent<OfficeBehaviour>().anomalyActive)
                {
                    rooms[currentRoom].GetComponent<OfficeBehaviour>().DeactivateAnomaly();
                    anomalyCounter--;
                    activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(true);
                    UpdateIntKey("RemovedCount");
                }
                else
                {
                    mistakeCounter++;
                    UpdateIntKey("MistakeCount");
                    if (mistakeCounter < 4)
                    {
                        activeCamera.GetComponent<CameraScreenBehaviour>().ShowResult(false);
                    }
                    if (mistakeCounter - 1 < mistakeSymbols.Count)
                    {
                        mistakeSymbols[(mistakeCounter - 1)].SetActive(false);
                    }
                }
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
        int roomNumber, checker = 0;
        bool activate = false;

        specialReady = false;

        if ((!GlobalResourcesTracker.animatronicUsage[0] && !GlobalResourcesTracker.animatronicUsage[4]))
        {
            while (1 == 1)
            {
                roomNumber = UnityEngine.Random.Range(0, rooms.Count);
                while ((roomNumber == this.GetComponent<CamereBehaviour>().currentCamera) || (roomNumber == previousRoom))
                {
                    roomNumber = UnityEngine.Random.Range(1, rooms.Count);
                }
                if (testSelector >= 0)
                {
                    roomNumber = testSelector;
                }

                switch (roomNumber)
                {
                    case 1:
                        if (!rooms[roomNumber].GetComponent<DiningHallBehaviour>().anomalyActive)
                        {
                            rooms[roomNumber].GetComponent<DiningHallBehaviour>().ActivateSpecialAbility();
                            activate = true;
                            break;
                        }
                        break;
                    case 2:
                        if (!rooms[roomNumber].GetComponent<BackRoomBehaviour>().anomalyActive)
                        {
                            rooms[roomNumber].GetComponent<BackRoomBehaviour>().ActivateSpecialAbility();
                            activate = true;
                            break;
                        }
                        break;
                    case 3:
                        if (!rooms[roomNumber].GetComponent<BathroomsBehaviour>().anomalyActive)
                        {
                            rooms[roomNumber].GetComponent<BathroomsBehaviour>().ActivateSpecialAbility();
                            activate = true;
                            break;
                        }
                        break;
                    case 4:
                        if (!rooms[roomNumber].GetComponent<ClosetBehaviour>().anomalyActive)
                        {
                            rooms[roomNumber].GetComponent<ClosetBehaviour>().ActivateSpecialAbility();
                            activate = true;
                            break;
                        }
                        break;
                    case 5:
                        if (!rooms[roomNumber].GetComponent<PirateCoveBehaviour>().anomalyActive)
                        {
                            rooms[roomNumber].GetComponent<PirateCoveBehaviour>().ActivateSpecialAbility();
                            activate = true;
                            break;
                        }
                        break;
                    case 6:
                        if (!rooms[roomNumber].GetComponent<RightHallwayBehaviour>().anomalyActive)
                        {
                            rooms[roomNumber].GetComponent<RightHallwayBehaviour>().ActivateSpecialAbility();
                            activate = true;
                            break;
                        }
                        break;
                    case 7:
                        if (!rooms[roomNumber].GetComponent<OfficeBehaviour>().anomalyActive)
                        {
                            rooms[roomNumber].GetComponent<OfficeBehaviour>().ActivateSpecialAbility();
                            activate = true;
                            break;
                        }
                        break;
                    default:
                        break;
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

        }
    }

    public override void CheckForSpecial()
    {
        if (specialCounter == specialLimiter)
        {
            specialReady = true;
            ResetSpecialCounter();
            GenerateSpecialInterval();
        }
    }

    public override void GenerateSpecialInterval()
    {
        specialTimer = 0.0f;
        specialLimiter = UnityEngine.Random.Range(2, 5);

        if (nightTimeReference >= 2 || nightTimeReference == 4)
        {
            specialInterval = UnityEngine.Random.Range(20.0f, 30.0f);
        }
        if (nightTimeReference >= 5)
        {
            specialInterval = UnityEngine.Random.Range(10.0f, 30.0f);
        }
    }

    public void SetPreviousRoom(int room) { previousRoom = room; }
}
