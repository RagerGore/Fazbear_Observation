using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHallwayBehaviour : RoomBehaviour
{
    [SerializeField] List<Material> changeMaterials;
    [SerializeField] GameObject deathVideo;

    List<Material> temp = new List<Material>();

    void Update()
    {
        if (CamereBehaviour.isReady && anomalyNumber == 6)
        {
            Items[11].SetActive(true);
            GlobalResourcesTracker.animatronicUsage[4] = true;
        }
    }

    public override void ActivateAnomaly()
    {
        //Check that the animatronic are present before choising a case number that uses them
        int caseNumber = UnityEngine.Random.Range(0, 12);
        while (((GlobalResourcesTracker.animatronicUsage[2] || GlobalResourcesTracker.animatronicUsage[6]) && (caseNumber >= 7))|| ((GlobalResourcesTracker.animatronicUsage[0] || GlobalResourcesTracker.animatronicUsage[4]) && caseNumber == 6) ||  caseNumber == previousAnomaly)
        {
            caseNumber = UnityEngine.Random.Range(0, 12);
        }
        if (testSelector >= 0)
        {
            caseNumber = testSelector;
        }

        switch (caseNumber)
        {
            case 0:
                //Posters Change
                for (int i = 0; i < 3; i++)
                {
                    temp = new List<Material>();
                    temp.Add(changeMaterials[3]);
                    Items[i].GetComponent<MeshRenderer>().SetMaterials(temp);
                }
                break;
            case 1:
                //It's Me over posters
                for (int i = 0; i < 3; i++)
                {
                    temp = new List<Material>();
                    temp.Add(changeMaterials[4]);
                    Items[i].GetComponent<MeshRenderer>().SetMaterials(temp);
                }
                Items[3].SetActive(true);
                break;
            case 2:
                //Add plant pots
                Items[6].SetActive(true);
                break;
            case 3:
                //Add extra cables
                Items[7].SetActive(true);
                break;
            case 4:
                //Crying children visit
                Items[8].SetActive(true);
                break;
            case 5:
                //Scatter drawings
                Items[9].SetActive(false);
                Items[10].SetActive(true);
                break;
            case 6:
                //Staring Freddy
                GameObject.FindWithTag("GameController").GetComponent<CamereBehaviour>().AddToWaitingList(6);
                break;
            case int n when n >= 7 && n <= 10:
                //Animatronic Visit
                Items[5].SetActive(true);
                GlobalResourcesTracker.animatronicUsage[6] = true;
                break;
            default: break;
        }

        anomalyActive = true;
        anomalyNumber = caseNumber;
    }

    public override void ActivateSpecialAbility()
    {
        //Select random Freddy variant for the room
        int caseNumber = UnityEngine.Random.Range(11, 12);
        caseNumber = 11;

        switch (caseNumber)
        {
            case 11:
                Items[4].SetActive(true);
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
            case 1:
                //Posters Change OR It's Me over posters
                for (int i = 0; i < 3; i++)
                {
                    temp = new List<Material>();
                    temp.Add(changeMaterials[i]);
                    Items[i].GetComponent<MeshRenderer>().SetMaterials(temp);
                }
                Items[3].SetActive(false);
                break;
            case 2:
                //Add plant pots
                Items[6].SetActive(false);
                break;
            case 3:
                //Add extra cables
                Items[7].SetActive(false);
                break;
            case 4:
                //Crying children visit
                Items[8].SetActive(false);
                break;
            case 5:
                //Scatter drawings
                Items[9].SetActive(true);
                Items[10].SetActive(false);
                break;
            case 6:
                //Staring Freddy
                Items[11].SetActive(false);
                GlobalResourcesTracker.animatronicUsage[4] = false;
                StopCoroutine("dyingSequence");
                deathVideo.SetActive(false);
                GameObject.FindWithTag("MainCamera").GetComponent<CameraScreenBehaviour>().SwitchButtons();
                Items[14].SetActive(true);
                Items[15].SetActive(true);
                CamereBehaviour.canPause = true;
                break;
            case int n when n >= 7 && n <= 10:
                Items[5].SetActive(false);
                GlobalResourcesTracker.animatronicUsage[6] = false;
                break;
            case 11:
                Items[4].SetActive(false);
                break;
            default: break;
        }

        if (anomalyNumber >= 11)
        {
            GlobalResourcesTracker.animatronicUsage[4] = false;
            GameObject.FindWithTag("GameController").GetComponent<Level1AnomalyBehaviour>().SetPreviousRoom(6);
            GameObject.FindWithTag("GameController").GetComponent<Level1AnomalyBehaviour>().IncreaseSpecialCounter();
            GameObject.FindWithTag("GameController").GetComponent<Level1AnomalyBehaviour>().SetSpecialReady(true);
        }

        previousAnomaly = anomalyNumber;
        anomalyActive = false;
        anomalyNumber = -1;
    }
}
