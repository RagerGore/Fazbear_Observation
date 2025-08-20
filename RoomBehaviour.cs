using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [SerializeField] string roomName;
    [SerializeField] protected int testSelector;

    public List<GameObject> Items = new List<GameObject>();
    public bool anomalyActive = false;
    protected int anomalyNumber;
    protected int previousAnomaly;

    public virtual void ActivateAnomaly()
    {
        //Randomly choose an anomaly to activate the flag this room as anomalous and keep the anomaly ID for removal.
    }

    public virtual void ActivateSpecialAbility()
    {
        //Activate the ability specific to this level in this room
    }

    public virtual void DeactivateAnomaly() 
    {
        //Look for specific anomaly using the anomaly number and remove it.
    }

    public string GetRoomName(){ return roomName; }
}
