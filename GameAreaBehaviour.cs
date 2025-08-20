using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAreaBehaviour : RoomBehaviour
{
    void Update()
    {
        //Update the animatronics based on if they are being used or not
        if (GlobalResourcesTracker.animatronicUsage[20])
        {
            Items[0].SetActive(false);
        }
        else
        {
            Items[0].SetActive(true);
        }
    }

    public override void ActivateAnomaly()
    {
        //Randomly choose an anomaly to activate the flag this room as anomalous and keep the anomaly ID for removal.
        int caseNumber = UnityEngine.Random.Range(0, 9);
        //Check that the animatronic are available before choosing a case number that uses them
        //while ((GlobalResourcesTracker.animatronicUsage[0] && (caseNumber == 0 || caseNumber == 1)) || caseNumber == previousAnomaly)
        //{
        //    caseNumber = UnityEngine.Random.Range(0, 9);
        //}
        if (testSelector >= 0)
        {
            caseNumber = testSelector;
        }

        //Randomly choose an anomaly to activate the flag this room as anomalous and keep the anomaly ID for removal.
        switch (caseNumber)
        {
            case 0:
                //Merry-Go-Round Movement
                Items[1].SetActive(false);
                Items[2].SetActive(true);
                break;
            case 1:
                break;
            default: break;
        }
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
                //Merry-Go-Round Movement
                Items[1].SetActive(true);
                Items[2].SetActive(false);
                break;
            default: break;
        }

        previousAnomaly = anomalyNumber;
        anomalyActive = false;
        anomalyNumber = -1;
    }
}
