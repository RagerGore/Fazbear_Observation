using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DeadBody : MonoBehaviour
{
    [SerializeField] GameObject flickeringLight;
    private float counter = 0.0f, inteval = 0.3f;

    void Update()
    {
        counter += Time.deltaTime;

        if (counter >= inteval)
        {
            int r = UnityEngine.Random.Range(0, 2);
            if(r == 0)
            {
                flickeringLight.SetActive(false);
            }
            else
            {
                flickeringLight.SetActive(true);
            }
            counter = 0.0f;
        }
    }
}
