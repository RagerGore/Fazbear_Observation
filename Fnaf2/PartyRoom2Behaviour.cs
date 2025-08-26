using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyRoom2Behaviour : RoomBehaviour
{
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
