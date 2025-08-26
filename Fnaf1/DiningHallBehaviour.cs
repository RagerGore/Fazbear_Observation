using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiningHallBehaviour : RoomBehaviour
{
    [SerializeField] List<Material> changeMaterials;

    List<Material> temp = new List<Material>();

    public override void ActivateAnomaly()
    {
        //Check that the animatronic are present before choisinga case number that uses them
        int caseNumber = UnityEngine.Random.Range(0, 11);
        while(((GlobalResourcesTracker.animatronicUsage[1]|| GlobalResourcesTracker.animatronicUsage[5]) && (GlobalResourcesTracker.animatronicUsage[2]|| GlobalResourcesTracker.animatronicUsage[6]) && caseNumber >= 6)|| caseNumber == previousAnomaly)
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
                //Removes a random table
                int r = UnityEngine.Random.Range(0, 6);
                Items[r].SetActive(false);
                break;
            case 1:
                //Change floor colour
                temp = new List<Material>();
                temp.Add(changeMaterials[0]);
                Items[6].GetComponent<MeshRenderer>().SetMaterials(temp);
                break;
            case 2:
                //Scatter party arts
                for(int i = 7;i < 13 ; i++)
                {
                    Items[i].SetActive(false);
                }
                for (int i = 13; i < 19; i++)
                {
                    Items[i].SetActive(true);
                }
                break;
            case 3:
                //Change table cloth
                temp = new List<Material>();
                temp.Add(changeMaterials[2]);
                for(int i = 19; i < 25; i++)
                {
                    Items[i].GetComponent<MeshRenderer>().SetMaterials(temp);
                }
                break;
            case 4:
                //Crying child table
                Items[4].SetActive(false);
                Items[25].SetActive(true);
                break;
            case 5:
                //Chairs Dissapear
                for (int i = 0; i < 6; i++)
                {
                    int x = UnityEngine.Random.Range(26, 33);
                    Items[x].SetActive(false);
                }
                break;
            case int n when n >= 6:
                //Animatronic Visit
                int j = UnityEngine.Random.Range(33, 35);
                while(GlobalResourcesTracker.animatronicUsage[j - 32] || GlobalResourcesTracker.animatronicUsage[j - 28])
                {
                    j = UnityEngine.Random.Range(33, 35);
                }
                Items[j].SetActive(true);
                GlobalResourcesTracker.animatronicUsage[j - 28] = true;
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

        switch(caseNumber)
        {
            case 11:
                Items[32].SetActive(true);
                break;
            case 12:
                Items[35].SetActive(true);
                break;
            default : break;
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
                //Removes a random table
                for (int i = 0; i < 6; i++)
                {
                    Items[i].SetActive(true);
                }
                break;
            case 1:
                //Change floor colour
                temp = new List<Material>();
                temp.Add(changeMaterials[1]);
                Items[6].GetComponent<MeshRenderer>().SetMaterials(temp);
                break;
            case 2:
                //Scatter party arts
                for (int i = 7; i < 13; i++)
                {
                    Items[i].SetActive(true);
                }
                for (int i = 13; i < 19; i++)
                {
                    Items[i].SetActive(false);
                }
                break;
            case 3:
                //Change table cloth
                temp = new List<Material>();
                temp.Add(changeMaterials[3]);
                for (int i = 19; i < 25; i++)
                {
                    Items[i].GetComponent<MeshRenderer>().SetMaterials(temp);
                }
                break;
            case 4:
                //Crying child table
                Items[4].SetActive(true);
                Items[25].SetActive(false);
                break;
            case 5:
                //Chairs Dissapear
                for (int i = 26; i < 32; i++)
                {
                    Items[i].SetActive(true);
                }
                break;
            case int n when n >= 6 &&  n <=10:
                //Animatronic Visit
                for (int i = 32; i<35; i++)
                {
                    int temp = -1; 
                    if(Items[i].activeSelf)
                    {
                        Items[i].SetActive(false);
                        temp = i - 28;
                    }
                    switch (temp)
                    {
                        case 4:
                            GlobalResourcesTracker.animatronicUsage[4] = false;
                            break;
                        case 5:
                            GlobalResourcesTracker.animatronicUsage[5] = false;
                            break;
                        case 6:
                            GlobalResourcesTracker.animatronicUsage[6] = false;
                            break;
                        default: break;
                    }
                }
                break;
            //Freddy Appearances
            case 11:
                Items[32].SetActive(false);
                break;
            case 12:
                Items[35].SetActive(false);
                break;
            default: break;
        }

        if(anomalyNumber >= 11)
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
