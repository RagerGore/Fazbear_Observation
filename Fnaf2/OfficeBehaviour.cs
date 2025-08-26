using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office2Behaviour : RoomBehaviour
{
    private int currentVisitingAnimatronic;

    void Update()
    {
        //Update the animatronics based on if they are being used or not
        if (GlobalResourcesTracker.animatronicUsage[4])
        {
            Items[8].SetActive(true);
        }
        else
        {
            Items[8].SetActive(false);
        }

        if (GlobalResourcesTracker.animatronicUsage[5])
        {
            Items[9].SetActive(true);
        }
        else
        {
            Items[9].SetActive(false);
        }

        if (GlobalResourcesTracker.animatronicUsage[6])
        {
            Items[10].SetActive(true);
        }
        else
        {
            Items[10].SetActive(false);
        }

        if (GlobalResourcesTracker.animatronicUsage[7])
        {
            Items[11].SetActive(true);
        }
        else
        {
            Items[11].SetActive(false);
        }

        if (GlobalResourcesTracker.animatronicUsage[12])
        {
            Items[12].SetActive(true);
        }
        else
        {
            Items[12].SetActive(false);
        }

        if (GlobalResourcesTracker.animatronicUsage[13])
        {
            Items[13].SetActive(true);
        }
        else
        {
            Items[13].SetActive(false);
        }

        if (GlobalResourcesTracker.animatronicUsage[14])
        {
            Items[14].SetActive(true);
        }
        else
        {
            Items[14].SetActive(false);
        }

        if (GlobalResourcesTracker.animatronicUsage[15])
        {
            Items[15].SetActive(true);
        }
        else
        {
            Items[15].SetActive(false);
        }

        if (GlobalResourcesTracker.animatronicUsage[20])
        {
            Items[16].SetActive(true);
        }
        else
        {
            Items[16].SetActive(false);
        }

        if (GlobalResourcesTracker.animatronicUsage[21])
        {
            Items[17].SetActive(true);
        }
        else
        {
            Items[17].SetActive(false);
        }

        if (GlobalResourcesTracker.animatronicUsage[22])
        {
            Items[18].SetActive(true);
        }
        else
        {
            Items[18].SetActive(false);
        }

        if (GlobalResourcesTracker.animatronicUsage[23])
        {
            Items[19].SetActive(true);
        }
        else
        {
            Items[19].SetActive(false);
        }
    }

    public override void ActivateAnomaly()
    {
        int caseNumber = UnityEngine.Random.Range(0, 11);

        int visitingAnimatronic = UnityEngine.Random.Range(0, 12);
        switch (visitingAnimatronic)
        {
            case 0:
            case 1:
            case 2:
            case 3: break;
            case 4: visitingAnimatronic = 8; break;
            case 5: visitingAnimatronic = 9; break;
            case 6: visitingAnimatronic = 10; break;
            case 7: visitingAnimatronic = 11; break;
            case 8: visitingAnimatronic = 16; break;
            case 9: visitingAnimatronic = 17; break;
            case 10: visitingAnimatronic = 18; break;
            case 11: visitingAnimatronic = 19; break;
            default: visitingAnimatronic = 0; break;    
        }

        //Check that the animatronic are available before choosing a case number that uses them
        while (caseNumber == previousAnomaly||(GlobalResourcesTracker.animatronicUsage[visitingAnimatronic] && caseNumber >= 7))
        {
            caseNumber = UnityEngine.Random.Range(0, 11);
        }
        if (testSelector >= 0)
        {
            caseNumber = testSelector;
        }

        //Randomly choose an anomaly to activate the flag this room as anomalous and keep the anomaly ID for removal.
        switch (caseNumber)
        {
            case 0:
                //Shadow Bonnie
                Items[0].SetActive(true);
                break;
            case 1:
                //Phone change
                Items[1].SetActive(false);
                Items[2].SetActive(true);
                break;
            case 2:
                //Poster change
                Items[3].SetActive(false);
                Items[4].SetActive(true);
                break;
            case 3:
                //Anomaly Locker
                Items[20].SetActive(false);
                Items[21].SetActive(true);
                break;
            case 4:
                //Extra Office Chair
                Items[22].SetActive(true);
                break;
            case 5:
                //Cups Movement
                Items[24].SetActive(false);
                Items[25].SetActive(true);
                break;
            case 6:
                //Golden Freddy
                Items[23].SetActive(true);
                break;
            case int n when n >= 7:
                //Animatronic visit
                Items[5].SetActive(false);
                Items[6].SetActive(true);
                Items[7].SetActive(true);
                currentVisitingAnimatronic = visitingAnimatronic;
                GlobalResourcesTracker.animatronicUsage[visitingAnimatronic] = true;
                break;
            default: break;
        }

        anomalyActive = true;
        anomalyNumber = caseNumber;
    }

    public override void ActivateSpecialAbility()
    {
        //Activate the ability specific to this level in this room
    }

    public override void DeactivateAnomaly()
    {
        //Look for specific anomaly using the anomaly number and remove it.
        switch (anomalyNumber)
        {
            case 0:
                //Shadow Bonnie
                Items[0].SetActive(false);
                break;
            case 1:
                Items[1].SetActive(true);
                Items[2].SetActive(false);
                break;
            case 2:
                //Poster change
                Items[3].SetActive(true);
                Items[4].SetActive(false);
                break;
            case 3:
                //Anomaly Locker
                Items[20].SetActive(true);
                Items[21].SetActive(false);
                break;
            case 4:
                //Extra Office Chair
                Items[22].SetActive(false);
                break;
            case 5:
                //Cups Movement
                Items[24].SetActive(true);
                Items[25].SetActive(false);
                break;
            case 6:
                //Golden Freddy
                Items[23].SetActive(true);
                break;
            case int n when n >= 7:
                //Animatronic visit
                Items[5].SetActive(true);
                Items[6].SetActive(false);
                Items[7].SetActive(false);
                GlobalResourcesTracker.animatronicUsage[currentVisitingAnimatronic] = false;
                break;
            default: break;
        }

        previousAnomaly = anomalyNumber;
        anomalyActive = false;
        anomalyNumber = -1;
    }
}
