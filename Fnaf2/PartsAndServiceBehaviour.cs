using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsAndServiceBehaviour : RoomBehaviour
{
    void Update()
    {
        //Update the animatronics based on if they are being used or not
        if (GlobalResourcesTracker.animatronicUsage[12])
        {
            Items[0].SetActive(false);
        }
        else
        {
            Items[0].SetActive(true);
        }

        if (GlobalResourcesTracker.animatronicUsage[13])
        {
            Items[1].SetActive(false);
        }
        else
        {
            Items[1].SetActive(true);
        }

        if (GlobalResourcesTracker.animatronicUsage[14])
        {
            Items[2].SetActive(false);
        }
        else
        {
            Items[2].SetActive(true);
        }

        if (GlobalResourcesTracker.animatronicUsage[15])
        {
            Items[3].SetActive(false);
        }
        else
        {
            Items[3].SetActive(true);
        }
    }

    public override void ActivateAnomaly()
    {
        //Randomly choose an anomaly to activate the flag this room as anomalous and keep the anomaly ID for removal.
    }

    public override void ActivateSpecialAbility()
    {
        //Activate the ability specific to this level in this room
    }

    public override void DeactivateAnomaly()
    {
        //Look for specific anomaly using the anomaly number and remove it.
    }
}
