using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStageBehaviour : RoomBehaviour
{
    [SerializeField] SkinnedMeshRenderer freddyRenderer,bonnieRenderer,chicaRenderer;
    [SerializeField] List<Material> changeMaterials;

    public List<Vector3> anomalyRotations = new List<Vector3>();
    List<Material> temp = new List<Material>();

    void Awake()
    {
        GameObject.FindWithTag("GameController").GetComponent<GlobalResourcesTracker>().ClearAnimatronicFlags();
    }

    void Update()
    {
        //Update the animatronics based on if they are being used or not
        if (GlobalResourcesTracker.animatronicUsage[4])
        {
            Items[11].SetActive(false);
        }
        else
        {
            Items[11].SetActive(true);
        }

        if (GlobalResourcesTracker.animatronicUsage[5])
        {
            Items[12].SetActive(false);
        }
        else
        {
            Items[12].SetActive(true);
        }

        if (GlobalResourcesTracker.animatronicUsage[6])
        {
            Items[13].SetActive(false);
        }
        else
        {
            Items[13].SetActive(true);
        }
    }

    public override void ActivateAnomaly()
    {
        int caseNumber = UnityEngine.Random.Range(0, 11);
        //Check that the animatronic are available before choosing a case number that uses them
        while (GlobalResourcesTracker.animatronicUsage[4] && (caseNumber == 3 || caseNumber == 6))
        {
            caseNumber = UnityEngine.Random.Range(0, 11);
        }
        while ((GlobalResourcesTracker.animatronicUsage[4] && GlobalResourcesTracker.animatronicUsage[5] && GlobalResourcesTracker.animatronicUsage[6] && (caseNumber == 0 || caseNumber == 1 || caseNumber == 3 || caseNumber == 6 || caseNumber == 7)) || caseNumber == previousAnomaly || ((GlobalResourcesTracker.animatronicUsage[3] || GlobalResourcesTracker.animatronicUsage[7]) && caseNumber >= 8))
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
                //Animatronics look at camera
                for (int i = 0; i < 3; i++)
                {
                    int r = UnityEngine.Random.Range(0, 3);
                    while (GlobalResourcesTracker.animatronicUsage[r+4])
                    {
                        r = UnityEngine.Random.Range(0, 3);
                    }
                    Items[r].transform.Rotate (anomalyRotations[r]);
                    GlobalResourcesTracker.animatronicUsage[r] = true;
                }
                break;
            case 1:
                //Animatronics accessories dissapear
                for (int i = 3; i < 6; i++)
                {
                    bool activate = false;
                    if (!GlobalResourcesTracker.animatronicUsage[i+1])
                    {
                        activate = true;
                    }

                    if (activate)
                    {
                        Items[i].SetActive(false);
                        GlobalResourcesTracker.animatronicUsage[i-3] = true;
                    }
                }
                break;
            case 2:
                //Cloud decorations dissapear
                for (int i = 6; i < 8; i++)
                {
                    Items[i].SetActive(false);
                }
                break;
            case 3:
                //Changes Freddy's hand microphone for a stand
                Items[3].SetActive(false);
                Items[8].SetActive(true);
                GlobalResourcesTracker.animatronicUsage[0] = true;
                break;
            case 4:
                //Extra star decorations
                Items[9].SetActive(true);
                break;
            case 5:
                //Background decoration changes
                Items[6].SetActive(false);
                Items[10].SetActive(true);
                break;
            case 6:
                //Changes Freddy to Golden Freddy
                temp = new List<Material>();
                temp.Add(changeMaterials[1]);
                temp.Add(freddyRenderer.materials[1]);
                temp.Add(changeMaterials[0]);
                freddyRenderer.SetMaterials(temp);
                GlobalResourcesTracker.animatronicUsage[0] = true;
                break;
            case 7:
                //Glitchout Animatronic's Material
                for (int i = 0; i < 3; i++)
                {
                    switch (i)
                    {
                        case 0:
                            temp = new List<Material>();
                            temp.Add(freddyRenderer.materials[0]);
                            temp.Add(freddyRenderer.materials[1]);
                            temp.Add(changeMaterials[6]);
                            freddyRenderer.SetMaterials(temp);
                            GlobalResourcesTracker.animatronicUsage[0] = true;
                            break;
                        case 1:
                            temp = new List<Material>();
                            temp.Add(changeMaterials[6]);
                            temp.Add(bonnieRenderer.materials[1]);
                            bonnieRenderer.SetMaterials(temp);
                            GlobalResourcesTracker.animatronicUsage[1] = true;
                            break;
                        case 2:
                            temp = new List<Material>();
                            temp.Add(changeMaterials[6]);
                            temp.Add(chicaRenderer.materials[1]);
                            chicaRenderer.SetMaterials(temp);
                            GlobalResourcesTracker.animatronicUsage[2] = true;
                            break;
                        default: break;
                    }
                }
                break;
            case int n when n >= 8:
                //Visiting Foxy
                Items[14].SetActive(true);
                GlobalResourcesTracker.animatronicUsage[7] = true;
                break;
            default: break;
        }

        anomalyActive = true;
        anomalyNumber = caseNumber;
    }

    public override void DeactivateAnomaly()
    {
        switch (anomalyNumber)
        {
            case 0:
                for (int i = 0; i < 3; i++)
                {
                    Items[i].transform.rotation = new Quaternion(0,0,0,0);
                    GlobalResourcesTracker.animatronicUsage[i] = false;
                }
                break;
            case 1:
                for (int i = 3; i < 6; i++)
                {
                    Items[i].SetActive(true);
                    GlobalResourcesTracker.animatronicUsage[i-3] = false;
                }
                break;
            case 2:
                for (int i = 6; i < 8; i++)
                {
                    Items[i].SetActive(true);
                }
                break;
            case 3:
                Items[3].SetActive(true);
                Items[8].SetActive(false);
                GlobalResourcesTracker.animatronicUsage[0] = false;
                break;
            case 4:
                Items[9].SetActive(false);
                break;
            case 5:
                Items[6].SetActive(true);
                Items[10].SetActive(false);
                break;
            case 6:
                temp = new List<Material>();
                temp.Add(changeMaterials[3]);
                temp.Add(freddyRenderer.materials[1]);
                temp.Add(changeMaterials[2]);
                freddyRenderer.SetMaterials(temp);
                GlobalResourcesTracker.animatronicUsage[0] = false;
                break;
            case 7:
                for (int i = 0; i < 3; i++)
                {
                    switch (i)
                    {
                        case 0:
                            temp = new List<Material>();
                            temp.Add(changeMaterials[3]);
                            temp.Add(freddyRenderer.materials[1]);
                            temp.Add(changeMaterials[2]);
                            freddyRenderer.SetMaterials(temp);
                            GlobalResourcesTracker.animatronicUsage[0] = false;
                            break;
                        case 1:
                            temp = new List<Material>();
                            temp.Add(changeMaterials[4]);
                            temp.Add(bonnieRenderer.materials[1]);
                            bonnieRenderer.SetMaterials(temp);
                            GlobalResourcesTracker.animatronicUsage[1] = false;
                            break;
                        case 2:
                            temp = new List<Material>();
                            temp.Add(changeMaterials[5]);
                            temp.Add(chicaRenderer.materials[1]);
                            chicaRenderer.SetMaterials(temp);
                            GlobalResourcesTracker.animatronicUsage[2] = false;
                            break;
                        default: break;
                    }
                }
                break;
            case int n when n >= 8:
                Items[14].SetActive(false);
                GlobalResourcesTracker.animatronicUsage[7] = false;
                break;
            default: break;
        }

        previousAnomaly = anomalyNumber;
        anomalyActive = false;
        anomalyNumber = -1;
    }
}
