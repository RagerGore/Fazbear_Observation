using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsParty : MonoBehaviour
{
    [SerializeField] List<GameObject> lights = new List<GameObject>();

    private int currentLight = 0;
    private float counter = 0.0f, inteval = 0.5f;

    void Update()
    {
        counter += Time.deltaTime;

        if (counter >= inteval)
        {
            currentLight++;
            if(currentLight >= lights.Count)
            {
                currentLight = 0;
            }
            for (int i = 0; i < lights.Count; i++)
            {
                lights[i].SetActive(false);
            }
            lights[currentLight].SetActive(true);
            counter = 0.0f;
        }
    }
}
