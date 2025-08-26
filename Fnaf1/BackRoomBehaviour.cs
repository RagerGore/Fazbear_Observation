using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRoomBehaviour : RoomBehaviour
{

    public override void ActivateAnomaly()
    {
        //Check that the animatronic are present before choisinga case number that uses them
        int caseNumber = UnityEngine.Random.Range(0, 8);
        if(((GlobalResourcesTracker.animatronicUsage[1]|| GlobalResourcesTracker.animatronicUsage[5]) && caseNumber==4)|| caseNumber == previousAnomaly)
        {
            caseNumber = UnityEngine.Random.Range(0, 8);
        }
        if (testSelector >= 0)
        {
            caseNumber = testSelector;
        }

        switch (caseNumber)
        {
            case 0:
                //Endo Looks at camera
                Items[0].SetActive(false);
                Items[1].SetActive(true);
                break;
            case 1:
                //Game Over Freddy instead of endo
                Items[0].SetActive(false);
                Items[2].SetActive(true);
                break;
            case 2:
                //Door wording changes
                Items[3].SetActive(false);
                Items[4].SetActive(true);
                break;
            case 3:
                //Add extra head
                Items[5].SetActive(true);
                break;
            case 4:
                //Bonnie Visit
                Items[6].SetActive(true);
                GlobalResourcesTracker.animatronicUsage[5] = true;
                break;
            case 5:
                //Door Closes
                Items[7].transform.rotation = new Quaternion(0, 0, 0, 0);
                break;
            case 6:
                //Sparky the dog appearence
                Items[8].SetActive(true);
                break;
            case 7:
                //Changes heads
                Items[9].SetActive(false);
                Items[10].SetActive(true);
                break;
            default: break;
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
                Items[11].SetActive(true);
                break;
            case 12:
                Items[12].SetActive(true);
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
                //Endo Looks at camera
                Items[0].SetActive(true);
                Items[1].SetActive(false);
                break;
            case 1:
                //Game Over Freddy instead of endo
                Items[0].SetActive(true);
                Items[2].SetActive(false);
                break;
            case 2:
                //Door wording changes
                Items[3].SetActive(true);
                Items[4].SetActive(false);
                break;
            case 3:
                //Add extra head
                Items[5].SetActive(false);
                break;
            case 4:
                //Bonnie Visit
                Items[6].SetActive(false);
                GlobalResourcesTracker.animatronicUsage[5] = false;
                break;
            case 5:
                //Door Closes
                Items[7].transform.Rotate(new Vector3(0, 81.283f, 0));
                break;
            case 6:
                //Sparky the dog appearence
                Items[8].SetActive(false);
                break;
            case 7:
                //Changes heads
                Items[9].SetActive(true);
                Items[10].SetActive(false);
                break;
            //Freddy Appearances
            case 11:
                Items[11].SetActive(false);
                break;
            case 12:
                Items[12].SetActive(false);
                break;
            default: break;
        }

        if (anomalyNumber >= 11)
        {
            GlobalResourcesTracker.animatronicUsage[4] = false;
            GameObject.FindWithTag("GameController").GetComponent<Level1AnomalyBehaviour>().SetPreviousRoom(2);
            GameObject.FindWithTag("GameController").GetComponent<Level1AnomalyBehaviour>().IncreaseSpecialCounter();
            GameObject.FindWithTag("GameController").GetComponent<Level1AnomalyBehaviour>().SetSpecialReady(true);
        }

        previousAnomaly = anomalyNumber;
        anomalyActive = false;
        anomalyNumber = -1;
    }
}