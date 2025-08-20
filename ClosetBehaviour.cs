using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetBehaviour : RoomBehaviour
{
    public override void ActivateAnomaly()
    {
        int caseNumber = UnityEngine.Random.Range(0, 11);
        //Check that the animatronic are available before choosing a case number that uses them
        while (((GlobalResourcesTracker.animatronicUsage[1]|| GlobalResourcesTracker.animatronicUsage[5]) && caseNumber >= 7)|| caseNumber == previousAnomaly)
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
                //Lightbulb breaks
                Items[0].SetActive(false);
                Items[1].SetActive(true);
                break;
            case 1:
                //Extra brooms
                Items[2].SetActive(true);
                break;
            case 2:
                //Door opens with silouette
                Items[3].transform.Rotate(new Vector3(0, 81.283f, 0));
                Items[4].SetActive(true);
                break;
            case 3:
                //Add speakers
                Items[10].SetActive(true);
                break;
            case 4:
                //Remove mop in bucket
                Items[6].SetActive(false);
                break;
            case 5:
                //Add vacuum cleaner
                Items[7].SetActive(true);
                break;
            case 6:
                //Cabinet changes to electrical box
                Items[8].SetActive(false);
                Items[9].SetActive(true);
                break;
            case int n when n>=7 && n <= 10:
                //Animatronic Visit
                Items[5].SetActive(true);
                GlobalResourcesTracker.animatronicUsage[5] = true;
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

        switch (caseNumber)
        {
            case 11:
                Items[11].SetActive(true);
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
                //Lightbulb breaks
                Items[0].SetActive(true);
                Items[1].SetActive(false);
                break;
            case 1:
                //Extra brooms
                Items[2].SetActive(false);
                break;
            case 2:
                //Door opens with silouette
                Items[3].transform.rotation = new Quaternion(0, 0, 0, 0);
                Items[4].SetActive(false);
                break;
            case 3:
                //Add speakers
                Items[10].SetActive(false);
                break;
            case 4:
                //Remove mop in bucket
                Items[6].SetActive(true);
                break;
            case 5:
                //Add vacuum cleaner
                Items[7].SetActive(false);
                break;
            case 6:
                //Cabinet changes to electrical box
                Items[8].SetActive(true);
                Items[9].SetActive(false);
                break;
            case int n when n >= 7 && n<=10:
                //Animatronic Visit
                Items[5].SetActive(false);
                GlobalResourcesTracker.animatronicUsage[5] = false;
                break;
            case 11:
                Items[11].SetActive(false);
                break;
            default: break;
        }

        if (anomalyNumber >= 11)
        {
            GlobalResourcesTracker.animatronicUsage[4] = false;
            GameObject.FindWithTag("GameController").GetComponent<Level1AnomalyBehaviour>().SetPreviousRoom(4);
            GameObject.FindWithTag("GameController").GetComponent<Level1AnomalyBehaviour>().IncreaseSpecialCounter();
            GameObject.FindWithTag("GameController").GetComponent<Level1AnomalyBehaviour>().SetSpecialReady(true);
        }

        previousAnomaly = anomalyNumber;
        anomalyActive = false;
        anomalyNumber = -1;
    }
}
