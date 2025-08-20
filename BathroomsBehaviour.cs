using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomsBehaviour : RoomBehaviour
{
    [SerializeField] List<Material> changeMaterials;

    List<Material> temp = new List<Material>();

    public override void ActivateAnomaly()
    {
        int caseNumber = UnityEngine.Random.Range(0, 11);
        //Check that the animatronic are available before choosing a case number that uses them
        while (((GlobalResourcesTracker.animatronicUsage[2]|| GlobalResourcesTracker.animatronicUsage[6]) && caseNumber >= 7) || caseNumber == previousAnomaly)
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
                //Floor sign changes
                Items[0].SetActive(false);
                Items[1].SetActive(true);
                break;
            case 1:
                //Poster in the back changes
                temp = new List<Material>();
                int r = UnityEngine.Random.Range(1, 5);
                temp.Add(changeMaterials[r]);
                Items[2].GetComponent<MeshRenderer>().SetMaterials(temp);
                break;
            case 2:
                //Cleaning Bot appears
                Items[12].SetActive(true);
                break;
            case 3:
                //Bright light appears
                Items[6].SetActive(true);
                break;
            case 4:
                //Pizza decor change
                Items[7].SetActive(false);
                Items[8].SetActive(true);
                break;
            case 5:
                //Extra objects
                Items[9].SetActive(true);
                break;
            case 6:
                //Gender signs change
                Items[10].SetActive(false);
                Items[11].SetActive(true);
                break;
            case int n when n>=7:
                //Animatronic visit
                int x = UnityEngine.Random.Range(0, 3);

                switch (x)
                {
                    case 0:
                        x = 4;
                        break;
                    case 1:
                        x = 5;
                        break;
                    case 2:
                        x = 13;
                        break;
                    default:
                        x = 4;
                        break;
                }

                Items[x].SetActive(true);

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
        int caseNumber = UnityEngine.Random.Range(11, 13);

        switch (caseNumber)
        {
            case 11:
                Items[3].SetActive(true);
                break;
            case 12:
                Items[14].SetActive(true);
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
                //Floor sign changes
                Items[0].SetActive(true);
                Items[1].SetActive(false);
                break;
            case 1:
                //Poster in the back changes
                temp = new List<Material>();
                temp.Add(changeMaterials[0]);
                Items[2].GetComponent<MeshRenderer>().SetMaterials(temp);
                break;
            case 2:
                //Cleaning Bot appears
                Items[12].SetActive(false);
                break;
            case 3:
                //Bright light appears
                Items[6].SetActive(false);
                break;
            case 4:
                //Pizza decor change
                Items[7].SetActive(true);
                Items[8].SetActive(false);
                break;
            case 5:
                //Extra objects
                Items[9].SetActive(false);
                break;
            case 6:
                //Gender signs change
                Items[10].SetActive(true);
                Items[11].SetActive(false);
                break;
            case int n when n >= 7 && n<=10:
                //Animatronic visit
                Items[4].SetActive(false);
                Items[5].SetActive(false);
                Items[13].SetActive(false);
                GlobalResourcesTracker.animatronicUsage[6] = false;
                break;
            case 11:
                Items[3].SetActive(false);
                break;
            case 12:
                Items[14].SetActive(false);
                break;
            default: break;
        }

        if (anomalyNumber >= 11)
        {
            GlobalResourcesTracker.animatronicUsage[4] = false;
            GameObject.FindWithTag("GameController").GetComponent<Level1AnomalyBehaviour>().SetPreviousRoom(3);
            GameObject.FindWithTag("GameController").GetComponent<Level1AnomalyBehaviour>().IncreaseSpecialCounter();
            GameObject.FindWithTag("GameController").GetComponent<Level1AnomalyBehaviour>().SetSpecialReady(true);
        }

        previousAnomaly = anomalyNumber;
        anomalyActive = false;
        anomalyNumber = -1;
    }
}