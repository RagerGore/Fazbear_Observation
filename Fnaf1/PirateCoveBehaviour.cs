using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateCoveBehaviour : RoomBehaviour
{
    [SerializeField] List<Material> changeMaterials;

    List<Material> temp = new List<Material>();

    void Update()
    {
        if(GlobalResourcesTracker.animatronicUsage[7])
        {
            Items[10].SetActive(false);
        }
        else
        {
            Items[10].SetActive(true);
        }
    }

    public override void ActivateAnomaly()
    {
        int caseNumber = UnityEngine.Random.Range(0, 9);
        //Check that the animatronic are available before choosing a case number that uses them
        while ((GlobalResourcesTracker.animatronicUsage[7] && (caseNumber == 2 || caseNumber == 7))|| caseNumber == previousAnomaly)
        {
            caseNumber = UnityEngine.Random.Range(0, 9);
        }
        if (testSelector >= 0)
        {
            caseNumber = testSelector;
        }

        switch (caseNumber)
        {
            case 0:
                //Foxy plush change
                Items[0].SetActive(false);
                Items[1].SetActive(true);
                break;
            case 1:
                //Sign changes to "It's Me"
                temp = new List<Material>();
                temp.Add(changeMaterials[1]);
                Items[2].GetComponent<MeshRenderer>().SetMaterials(temp);
                break;
            case 2:
                //Foxy Peeking
                Items[3].SetActive(false);
                Items[4].SetActive(true);
                GlobalResourcesTracker.animatronicUsage[3] = true;
                break;
            case 3:
                //Extra "Ball Pit"
                Items[5].SetActive(true);
                break;
            case 4:
                //Chalk board changes
                temp = new List<Material>();
                temp.Add(changeMaterials[3]);
                Items[6].GetComponent<MeshRenderer>().SetMaterials(temp);
                break;
            case 5:
                //Drinks and plates change position
                Items[7].transform.position = new Vector3(26.3269997f, -6.13399982f, 17.7010002f);
                Items[8].transform.position = new Vector3(26.2299995f, -6.09130001f, 16.1690006f);
                break;
            case 6:
                //Add endoskeleton to bin
                Items[9].SetActive(true);
                break;
            case 7:
                //Foxy grabbing plushie
                Items[3].SetActive(false);
                Items[0].SetActive(false);
                Items[11].SetActive(true);
                break;
            case 8:
                //Lights Party
                Items[12].SetActive(true);
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
        caseNumber = 11;

        switch (caseNumber)
        {
            case 11:
                Items[3].SetActive(false);
                Items[13].SetActive(true);
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
                //Foxy plush change
                Items[0].SetActive(true);
                Items[1].SetActive(false);
                break;
            case 1:
                //Sign changes to "It's Me"
                temp = new List<Material>();
                temp.Add(changeMaterials[0]);
                Items[2].GetComponent<MeshRenderer>().SetMaterials(temp);
                break;
            case 2:
                //Foxy Peeking
                Items[3].SetActive(true);
                Items[4].SetActive(false);
                GlobalResourcesTracker.animatronicUsage[3] = false;
                break;
            case 3:
                //Extra "Ball Pit"
                Items[5].SetActive(false);
                break;
            case 4:
                //Chalk board changes
                temp = new List<Material>();
                temp.Add(changeMaterials[2]);
                Items[6].GetComponent<MeshRenderer>().SetMaterials(temp);
                break;
            case 5:
                //Drinks and plates change position
                Items[7].transform.position = new Vector3(26.3269997f, -6.13399982f, 15.8120003f);
                Items[8].transform.position = new Vector3(26.2299995f, -6.09130001f, 18.0939999f);
                break;
            case 6:
                //Add endoskeleton to bin
                Items[9].SetActive(false);
                break;
            case 7:
                //Foxy grabbing plushie
                Items[3].SetActive(true);
                Items[0].SetActive(true);
                Items[11].SetActive(false);
                break;
            case 8:
                //Lights Party
                Items[12].SetActive(false);
                break;
            case 11:
                Items[3].SetActive(true);
                Items[13].SetActive(false);
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
