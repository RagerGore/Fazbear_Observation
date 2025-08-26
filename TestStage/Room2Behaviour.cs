using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2Behaviour : RoomBehaviour
{
    public override void ActivateAnomaly()
    {
        Items[1].SetActive(true);
        anomalyActive = true;
        //anomalyNumber = 0;
    }

    public override void DeactivateAnomaly()
    {
        Items[1].SetActive(false);
        anomalyActive = false;
        //anomalyNumber = null;
    }
}
