using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalResourcesTracker : MonoBehaviour
{
    //This list helps visualise the main list during testing
    [SerializeField] List<bool> visualAid = new List<bool>();

    /*This list will keep track of wich animatronic left it's original spot or not. The order will be as follows
     * 0 - Stage Freddy
     * 1 - Stage Bonnie
     * 2 - Stage Chica
     * 3 - Stage Foxy
     * 4 - (Stage)Visiting Freddy
     * 5 - (Stage)Visiting Bonnie
     * 6 - (Stage)Visiting Chica
     * 7 - (Stage)Visiting Foxy
     * 8 - Extra Freddy
     * 9 - Extra Bonnie
     * 10 - Extra Chica
     * 11 - Extra Foxy
     * 12 - (Extra)Visiting Freddy
     * 13 - (Extra)Visiting Bonnie
     * 14 - (Extra)Visiting Chica
     * 15 - (Extra)Visiting Foxy
     * 16 - Extra Animatronic
     * 17 - Extra Animatronic
     * 18 - Extra Animatronic
     * 19 - Extra Animatronic
     * 20 - Extra Visiting Animatronic 
     * 21 - Extra Visiting Animatronic 
     * 22 - Extra Visiting Animatronic 
     * 23 - Extra Visiting Animatronic 
    */
    static public List<bool> animatronicUsage = new List<bool>();

    void Start()
    {
        for (int i = 0; i < 24; i++)
        {
            animatronicUsage.Add(false);
        }
        ClearAnimatronicFlags();
    }

    void Update()
    {
        visualAid = animatronicUsage;

        //Test Overwriter
        //animatronicUsage = visualAid;
    }

    public void ClearAnimatronicFlags()
    {
        for (int i = 0; i < animatronicUsage.Count; i++)
        {
            animatronicUsage[i] = false;
        }
    }
}
