using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OfficeBehaviour : RoomBehaviour
{
    [SerializeField] AudioSource flickeringSound;

    void Update()
    {
        if (CamereBehaviour.isReady && anomalyNumber == 1)
        {
            Items[4].SetActive(true);
        }
        if (CamereBehaviour.isReady && anomalyNumber == 3)
        {
            Items[5].SetActive(true);
        }
        if (CamereBehaviour.isReady && anomalyNumber == 6)
        {
            Items[6].SetActive(true);
        }
    }

    public override void ActivateAnomaly()
    {
        int caseNumber = UnityEngine.Random.Range(0, 11);
        //Check that the animatronic are available before choosing a case number that uses them
        while ((((GlobalResourcesTracker.animatronicUsage[0] || GlobalResourcesTracker.animatronicUsage[4])&&(GlobalResourcesTracker.animatronicUsage[1] || GlobalResourcesTracker.animatronicUsage[5])&&(GlobalResourcesTracker.animatronicUsage[2] || GlobalResourcesTracker.animatronicUsage[6])&&(GlobalResourcesTracker.animatronicUsage[3] || GlobalResourcesTracker.animatronicUsage[7])) && caseNumber >= 7)||(caseNumber == previousAnomaly))
        {
            caseNumber = UnityEngine.Random.Range(0, 11);
        }
        if (testSelector >= 0)
        {
            caseNumber = testSelector;
        }

        switch (caseNumber)
        {
            case 0:
                //Adventure Toy Chica instead of Cupcake
                Items[7].SetActive(true);
                Items[8].SetActive(false);
                break;
            case 1:
                //Video playing on a screen
                GameObject.FindWithTag("GameController").GetComponent<CamereBehaviour>().AddToWaitingList(7);
                break;
            case 2:
                //Change Chair Position
                Items[1].SetActive(true);
                Items[2].SetActive(false);
                break;
            case 3:
                //Phone call
                GameObject.FindWithTag("GameController").GetComponent<CamereBehaviour>().AddToWaitingList(7);
                break;
            case 4:
                //Extra closed door
                Items[0].SetActive(true);
                break;
            case 5:
                //Dead body on chair
                Items[3].SetActive(true);
                flickeringSound.Play();
                break;
            case 6:
                //Golden freddy appearence
                GameObject.FindWithTag("GameController").GetComponent<CamereBehaviour>().AddToWaitingList(7);
                break;
            case int n when n>=7:
                //Animatronic Visit
                int x = UnityEngine.Random.Range(0, 4);
                switch (x)
                {
                    case 0:
                        Items[10].SetActive(true);
                        GlobalResourcesTracker.animatronicUsage[5] = true;
                        break;
                    case 1:
                        Items[11].SetActive(true);
                        Items[13].SetActive(false);
                        GlobalResourcesTracker.animatronicUsage[6] = true;
                        break;
                    case 2:
                        Items[12].SetActive(true);
                        GlobalResourcesTracker.animatronicUsage[7] = true;
                        break;
                    case 3:
                        Items[9].SetActive(true);
                        Items[10].SetActive(true);
                        Items[11].SetActive(true);
                        Items[12].SetActive(true);
                        Items[13].SetActive(false);
                        GlobalResourcesTracker.animatronicUsage[4] = true;
                        GlobalResourcesTracker.animatronicUsage[5] = true;
                        GlobalResourcesTracker.animatronicUsage[6] = true;
                        GlobalResourcesTracker.animatronicUsage[7] = true;
                        break;
                    default: break;
                }
                break;
            default:break;
        }

        anomalyActive = true;
        anomalyNumber = caseNumber;
    }

    public override void ActivateSpecialAbility()
    {
        //Select random Freddy variant for the room
        int caseNumber = UnityEngine.Random.Range(11, 13);

        switch (caseNumber)
        {
            case 11:
                Items[9].SetActive(true);
                break;
            default: break;
        }

        GlobalResourcesTracker.animatronicUsage[4] = true;

        anomalyActive = true;
        anomalyNumber = caseNumber;
    }

    public override void DeactivateAnomaly()
    {
        switch (anomalyNumber)
        {
            case 0:
                Items[7].SetActive(false);
                Items[8].SetActive(true);
                break;
            case 1:
                Items[4].SetActive(false);
                break;
            case 2:
                Items[1].SetActive(false);
                Items[2].SetActive(true);
                break;
            case 3:
                Items[5].SetActive(false);
                break;
            case 4:
                Items[0].SetActive(false);
                break;
            case 5:
                Items[3].SetActive(false);
                Items[14].SetActive(true);
                flickeringSound.Stop();
                break;
            case 6:
                Items[6].SetActive(false);
                GameObject.FindWithTag("MainCamera").GetComponent<CameraScreenBehaviour>().SwitchButtons();
                break;
            case int n when n>-7:
                Items[13].SetActive(true);
                for (int i=9;i<13;i++)
                {
                    if (Items[i].activeSelf)
                    {
                        Items[i].SetActive(false);
                        GlobalResourcesTracker.animatronicUsage[i-5] = false;
                    }
                }
                Items[13].SetActive(true);
                break;
            case 11:
                Items[9].SetActive(false);
                break;
            default: break;
        }

        if (anomalyNumber >= 11)
        {
            GlobalResourcesTracker.animatronicUsage[4] = false;
            GameObject.FindWithTag("GameController").GetComponent<Level1AnomalyBehaviour>().SetPreviousRoom(1);
            GameObject.FindWithTag("GameController").GetComponent<Level1AnomalyBehaviour>().IncreaseSpecialCounter();
            GameObject.FindWithTag("GameController").GetComponent<Level1AnomalyBehaviour>().SetSpecialReady(true);
        }

        previousAnomaly = anomalyNumber;
        anomalyActive = false;
        anomalyNumber = -1;
    }
}
