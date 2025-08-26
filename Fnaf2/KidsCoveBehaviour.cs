using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidsCoveBehaviour : RoomBehaviour
{
    void Update()
    {
        //Update the animatronics based on if they are being used or not
        if (GlobalResourcesTracker.animatronicUsage[7])
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
